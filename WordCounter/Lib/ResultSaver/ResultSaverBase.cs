using System.Collections.Generic;

namespace Lib.ResultSaver
{
    public abstract class ResultSaverBase: IResultSaver
    {
        private readonly Dictionary<string, int> _words = new Dictionary<string, int>();

        public void Add(string word, int count)
        {
            int oldCount;
            if (_words.TryGetValue(word, out oldCount))
                _words[word] = oldCount + count;
            else
                _words.Add(word, count);
        }

        protected Dictionary<string, int> Words => _words;

        public abstract void Save();
    }
}
