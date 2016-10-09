using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Test
{
    public class ResultSaverSpy: ResultSaverBase
    {
        public override void Save()
        {
            // Empty;
        }

        public IList<WordCountItem> Items
        {
            get
            {
                return Words.Keys.Select(word => new WordCountItem(word, Words[word])).ToList();
            }
        }
    }
}
