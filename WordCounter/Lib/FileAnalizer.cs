using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Lib
{
    /// <summary>
    /// обрабатывает один файл, а затем отправляет результат в очередь
    /// </summary>
    public class FileAnalizer
    {
        private readonly string _fileName;
        private readonly WordsCountQueue _queue;
        private readonly Action<Exception> _applExceptionHandler;

        public FileAnalizer(string fileName, WordsCountQueue queue, Action<Exception> applExceptionHandler)
        {
            _fileName = fileName;
            _queue = queue;
            _applExceptionHandler = applExceptionHandler;
        }

        public void Execute()
        {
            ThreadPool.QueueUserWorkItem(SafeRun);
        }

        private void SafeRun(object state)
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                _applExceptionHandler(ex);
            }
        }

        private void Run()
        {
            var result = new WordsCount();
            using (var fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs, Encoding.Default)) 
                {
                    var lineAnalizer = new LineAnalizer();
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var words = lineAnalizer.Pars(line);
                        foreach (var word in words)
                        {
                            result.AddWordFound(word.ToLower());
                        }
                    }
                }
            }
            _queue.Add(result);
        }
    }
}
