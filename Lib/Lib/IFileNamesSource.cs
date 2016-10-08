using System.Collections.Generic;

namespace Lib
{
    public interface IFileNamesSource
    {
        IList<string> FileNames { get; }
    }
}
