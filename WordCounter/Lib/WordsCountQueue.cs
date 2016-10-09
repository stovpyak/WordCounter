using System.Collections.Generic;
using System.Threading;

namespace Lib
{
    /// <summary>
    /// Очередь - в неё добавляют результаты обработчики файлов
    /// </summary>
    public class WordsCountQueue
    {
        private readonly Queue<WordsCount> _queue = new Queue<WordsCount>();
        private readonly object _lockOn = new object();

        public void Add(WordsCount wordses)
        {
            lock (_lockOn)
            {
                _queue.Enqueue(wordses);
            }
            ChangeEvent.Set();
        }

        public WordsCount GetItem()
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
