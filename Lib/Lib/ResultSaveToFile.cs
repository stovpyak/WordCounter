using System.IO;

namespace Lib
{
    public class ResultSaveToFile: ResultSaverBase
    {
        private readonly string _fileName;

        public ResultSaveToFile(string resultFileName)
        {
            _fileName = resultFileName;
        }

        public override void Save()
        {
            using (StreamWriter file = new StreamWriter(_fileName, false))
            {
                foreach (var word in Words)
                {
                    file.WriteLine($"{word.Key} {word.Value}");
                }
            }
        }
    }
}
