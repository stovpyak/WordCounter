using System.Collections.Generic;

namespace Lib
{
    public interface IResultTarget
    {
        void AddWordFound(string word);

        IList<WorkCount> Items { get; }
    }
}
