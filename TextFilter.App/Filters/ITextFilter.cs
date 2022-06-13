using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.App.Models;

namespace TextFilter.App.Filters
{
    public interface ITextFilter
    {
        public IList<Word> Filter(IList<Word> text);
        public string ApplyFilterLogic(string word);
    }
}
