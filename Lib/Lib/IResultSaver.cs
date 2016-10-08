namespace Lib
{
    /// <summary>
    /// Тот кто будет сохранять результат
    /// </summary>
    public interface IResultSaver
    {
        void Add(string word, int count);

        void Save();
    }
}
