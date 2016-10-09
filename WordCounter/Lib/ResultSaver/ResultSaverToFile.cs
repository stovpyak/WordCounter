using System.IO;
using System.Linq;

namespace Lib.ResultSaver
{
    public class ResultSaverToFile: ResultSaverBase
    {
        private readonly string _fileName;

        public ResultSaverToFile(string resultFileName)
        {
            _fileName = resultFileName;
        }

        public override void Save()
        {
            using (var file = new StreamWriter(_fileName, false))
            {
                var ordered = Words.OrderByDescending(w => w.Value);
                foreach (var word in ordered)
                {
                    file.WriteLine($"{word.Key} {word.Value}");
                }
            }
        }
    }
}
