using System;
using System.Threading;

namespace Lib
{
    /// <summary>
    /// Вся логика здесь:
    /// есть три сущности: WordsCountQueue, FileAnalizer и Marger - они работают в разных потоках
    /// FileAnalizer`ы распознают слова в одном файле и результат добавляет в очередь (FileAnalizer-ов много)
    /// Marger извлекает результат анализа одного файла из очереди и добавляет его в "общий" результат
    /// </summary>
    public class Appl
    {
        private readonly IFileNamesSource _fileNamesSource;
        private ManualResetEventSlim _stopEvent;

        public Appl(IFileNamesSource fileNamesSource)
        {
            _fileNamesSource = fileNamesSource;
        }

        public void Execute(IResultSaver saver)
        {
            var queue = new WordsCountQueue();
            _stopEvent = new ManualResetEventSlim(false);
            var marger = new Marger(_fileNamesSource.FileNames.Count, queue, saver, _stopEvent, ApplExceptionHandler);
            foreach (var fileName in _fileNamesSource.FileNames)
            {
                var analizer = new FileAnalizer(fileName, queue, ApplExceptionHandler);
                analizer.Execute();
            }
            // здесь выполнение остановится, пока (marger или exception) не просигнализирует об окончании работы
            _stopEvent.Wait();

            if (_wasException != null)
                throw new ApplicationException("Ошибка в дочернем потоке", _wasException);

            saver.Save();
        }

        private Exception _wasException;
        private void ApplExceptionHandler(Exception ex)
        {
            _wasException = ex;
            _stopEvent.Set();
        }

    }
}
