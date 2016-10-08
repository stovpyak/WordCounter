namespace Lib
{
    public class Appl
    {
        private readonly IFileNamesSource _fileNamesSource;

        public Appl(IFileNamesSource fileNamesSource)
        {
            _fileNamesSource = fileNamesSource;
        }

        public void Execute(IResultSaver saver)
        {
            foreach (var fileName in _fileNamesSource.FileNames)
            {
                var analizer = new FileAnalizer(fileName);
                var resultTarget = new ResultTarget();
                analizer.Execute(resultTarget);
                foreach (var item in resultTarget.Items)
                    saver.Add(item.Word, item.Count);
            }
            saver.Save();
        }
    }
}
