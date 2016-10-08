using System;
using System.IO;
using System.Text;

namespace Lib
{
    public class FileAnalizer
    {
        private readonly string _fileName;

        public FileAnalizer(string fileName)
        {
            _fileName = fileName;
        }

        public void Execute(IResultTarget target)
        {
            using (var fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs, Encoding.Default))
                {
                    string[] separators = new string[] { " ", "\r\n", "\t", "\"" };
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            target.AddWordFound(word.ToLower());
                        }
                    }
                }
            }
        }
    }
}
