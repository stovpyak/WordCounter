namespace Lib
{
    public struct WorkCountItem
    {
        public WorkCountItem(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }
    }
}
