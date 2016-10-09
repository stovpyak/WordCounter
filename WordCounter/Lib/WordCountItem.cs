namespace Lib
{
    public struct WordCountItem
    {
        public WordCountItem(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }
    }
}
