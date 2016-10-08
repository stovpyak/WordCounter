namespace Lib
{
    public struct WorkCount
    {
        public WorkCount(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }
    }
}
