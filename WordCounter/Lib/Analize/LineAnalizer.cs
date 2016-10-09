using System;

namespace Lib.Analize
{
    /// <summary>
    /// находит слова в одной строчке
    /// todo: с переносами слов есть проблема - перенесенные слова считаются как "новые"
    /// </summary>
    public class LineAnalizer
    {
        readonly string[] _separators = new string[] { " ", "\r\n", "\r", "\n", "\t", "\"", ",", ".", "!", "?", ":", "(", ")" };

        public string[] Pars(string line)
        {
            return line.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
