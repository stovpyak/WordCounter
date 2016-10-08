using System.Collections.Generic;

namespace Lib.Test
{
    public class FileNameSourceStub: IFileNamesSource
    {
        private readonly List<string> _fileNames = new List<string>(); 

        public void Add(string fileName)
        {
            _fileNames.Add(fileName);
        }

        public IList<string> FileNames => _fileNames;
    }
}
