using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.App.Models;

namespace TextFilter.App.Filters
{
    public class VowelFilter : TextFilter
    {
        public override IList<Word> Filter(IList<Word> text)
        {
            if (text.Any())
            {
                var words = text.Where(x => !x.IsPunctuation);
                foreach (var word in words)
                    word.Value = ApplyFilterLogic(word.Value);
            }

            if (nextFilter != null)
                return nextFilter.Filter(text);
            else
                return text;
        }

        public override string ApplyFilterLogic(string word)
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

            int wordCenter = word.Length / 2;
            string centerStr = word.Length % 2 == 1 ? word.Substring(wordCenter, 1) : word.Substring(wordCenter - 1, 2);

            foreach (char c in vowels)
                if (centerStr.Contains(char.ToLower(c)) || centerStr.Contains(char.ToUpper(c)))
                    return "";

            return word;
        }
    }
}
