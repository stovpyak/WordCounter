namespace Lib
{
    /// <summary>
    /// Тот кто будет сохранять результат.
    /// реализация может быть разной: в файл, в базу или spy для тестов
    /// </summary>
    public interface IResultSaver: IWords
    {
        void Save();
    }
}
