namespace Lib
{
    /// <summary>
    /// Тот кто будет сохранять результат
    /// </summary>
    public interface IResultSaver: IWords
    {
        void Save();
    }
}
