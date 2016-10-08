using System.Collections.Generic;

namespace Lib
{
    /// <summary>
    /// предоставляет список файлов
    /// реализаця может быть разной: мз командной строки, openDialog или stub в тестах
    /// </summary>
    public interface IFileNamesSource
    {
        IList<string> FileNames { get; }
    }
}
