using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.App.Models;

namespace TextFilter.App.Filters
{
    public class LengthFilter : TextFilter
    {
        private readonly int _minLength;

        public LengthFilter(int minLength) : base()
        {
            _minLength = minLength;
        }

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
            return word.Length < _minLength ? string.Empty : word;
        }
    }
}
