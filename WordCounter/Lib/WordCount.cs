using System.Collections.Generic;
using System.Linq;

namespace Lib
{
    public class WordCount
    {
        private readonly Dictionary<string, int> _words = new Dictionary<string, int>();

        public void AddWordFound(string word)
        {
            int count;
            if (_words.TryGetValue(word, out count))
                _words[word] = count + 1;
            else
                _words.Add(word, 1);
        }

        public IList<WorkCountItem> Items
        {
            get
            {
                return _words.Keys.Select(word => new WorkCountItem(word, _words[word])).ToList();
            }
        }
    }
}
