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
        private readonly WordCountQueue _queue;
        private readonly Action<Exception> _applExceptionHandler;

        public FileAnalizer(string fileName, WordCountQueue queue, Action<Exception> applExceptionHandler)
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
            // распознаватель слов пока "без изысков":
            // если слово в скобках (, то считает его "другим"
            // не распознает переносов на новую строку
            // запятые и др. знаки препинания не учитывает
            // ?
            var result = new WordCount();
            using (var fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs, Encoding.Default))
                {
                    string[] separators = new string[] { " ", "\r\n", "\t", "\"" };
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
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
