using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.App.Models;

namespace TextFilter.App.Filters
{
    public abstract class TextFilter : ITextFilter
    {
        protected ITextFilter nextFilter;

        public void SetNextFilter(ITextFilter filter)
        {
            nextFilter = filter;
        }

        public abstract IList<Word> Filter(IList<Word> text);
        
        public abstract string ApplyFilterLogic(string word);
    }
}
