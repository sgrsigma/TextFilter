using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextFilter.App.Filters;
using TextFilter.App.Models;


namespace TextFilter.App
{
    public class TextProcessor : IDisposable
    {
        private string originalText = string.Empty;
        private string filteredText = string.Empty;

        private ITextReader textReader;
        private IList<Word> words;

        public TextProcessor(ITextReader textReader)
        {
            this.textReader = textReader;
            words = new List<Word>();
        }

        public void Read(string filetPath)
        {
            using(var rd = textReader.StreamReader(filetPath))
            {
                originalText = rd.ReadToEnd();
            }
        }

        public void ExtractWords()
        {
            // gets all the words
            Regex reg = new Regex(@"\b([A-Za-z])\w+\b");
            var wordsCollection = reg.Matches(originalText);
            foreach (Match match in wordsCollection)
                words.Add(new Word(match.Value, match.Index, match.Length, false));

            // gets all the non-words (white space, punctuation, and etc)
            reg = new Regex(@"\W");
            var punctuationCollection = reg.Matches(originalText);
            foreach (Match match in punctuationCollection)
                words.Add(new Word(match.Value, match.Index, match.Length, true));

            if(words.Any())
                words = words.OrderBy(x => x.Index).ToList();
        }


        /// <summary>
        /// Three filters 
        /// 1. filter out words that contains vowels in the middle
        /// 2. filter out words that have length less than 3
        /// 3. filter ou words that contains the letter 't'
        /// </summary>
        public void Filter()
        {
            VowelFilter filter1 = new VowelFilter();
            LengthFilter filter2 = new LengthFilter(3);
            LetterFilter filter3 = new LetterFilter('t');
            
            filter1.SetNextFilter(filter2);
            filter2.SetNextFilter(filter3);

            words = filter1.Filter(words);
        }

        public void Merge()
        {
            if(!words.Any())
                filteredText = string.Empty;
            else
            {
                var results = words.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value);

                StringBuilder sb = new StringBuilder();
                foreach (var s in results)
                    sb.Append(s);

                filteredText = sb.ToString();
            }
        }

        public void Dispose()
        {
            words = null;
            originalText = string.Empty;
            filteredText = string.Empty;
        }

        public string OriginalText { get { return originalText; } }

        public string FilteredText { get { return filteredText; } }

        public IList<Word> Words
        {
            get { return words; }
            set { words = value; }
        }
    }
}
