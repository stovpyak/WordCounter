using System.Collections.Generic;
using System.Threading;

namespace Lib
{
    /// <summary>
    /// Очередь - в неё добавляют результаты обработчики файлов
    /// </summary>
    public class WordCountQueue
    {
        private readonly Queue<WordCount> _queue = new Queue<WordCount>();
        private readonly object _lockOn = new object();

        public void Add(WordCount words)
        {
            lock (_lockOn)
            {
                _queue.Enqueue(words);
            }
            ChangeEvent.Set();
        }

        public WordCount GetItem()
        {
            lock (_lockOn)
            {
                if (_queue.Count > 0)
                {
                    var result = _queue.Dequeue();
                    return result;
                }
                return null;
            }
        }

        public AutoResetEvent ChangeEvent { get; } = new AutoResetEvent(false);
    }
}
