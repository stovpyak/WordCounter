using System;
using System.Threading;

namespace Lib
{
    /// <summary>
    /// забирает результаты из очереди и добавляет их в общий "результат" 
    /// </summary>
    public class Marger
    {
        private readonly int _fileCount;
        private readonly WordsCountQueue _queue;
        private readonly IWords _totalResult;
        private readonly ManualResetEventSlim _stopEvent;
        private readonly Action<Exception> _applExceptionHandler;
        

        public Marger(int fileCount, WordsCountQueue queue, IWords totalResult, ManualResetEventSlim stopEvent, Action<Exception> applExceptionHandler)
        {
            _fileCount = fileCount;
            _queue = queue;
            _totalResult = totalResult;
            _stopEvent = stopEvent;
            _applExceptionHandler = applExceptionHandler;
            var innerThread = new Thread(Run) { Name = "Marger" };
            innerThread.Start();
        }

        private void Run()
        {
            try
            {
                SafeRun();
            }
            catch (Exception ex)
            {
                _applExceptionHandler(ex);
            }
        }

        private int _count;

        private void SafeRun()
        {
            try
            {
                while (!IsNeedStop)
                {
                    var words = _queue.GetItem();
                    if (words != null)
                    {
                        foreach (var item in words.Items)
                            _totalResult.Add(item.Word, item.Count);
                        _count ++;
                        if (_count >= _fileCount)
                        {
                            // сообщаем, что работа завершена
                            _stopEvent.Set();
                            _isNeedStop = true;
                        }
                    }
                    else
                        WaitQueue();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Произошла ошибка в Marger", ex);
            }
        }

        private bool IsNeedStop => _isNeedStop;

        private volatile bool _isNeedStop;

        protected void WaitQueue()
        {
            _queue.ChangeEvent.WaitOne();
        }
    }
}
