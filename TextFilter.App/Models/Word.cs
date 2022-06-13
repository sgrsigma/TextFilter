using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.App.Models
{
    public class Word
    {
        private string value;
        private readonly int index;
        private readonly int length;
        private readonly bool isPunctuation;

        public Word(string value, int index, int length, bool isPunctuation)
        {
            this.value = value;
            this.index = index;
            this.length = length;
            this.isPunctuation = isPunctuation;
        }

        public int Index { get { return index; } }
        public string Value 
        { 
            get { return value; }
            set { this.value = value; }
        }
        public int Length { get { return length; } }
        public bool IsPunctuation { get { return isPunctuation; } }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else
            {
                var toCompare = (Word)obj;
                return toCompare.index == this.index 
                    && toCompare.value == this.value 
                    && toCompare.length == this.length 
                    && toCompare.isPunctuation == this.isPunctuation;
            }
        }
    }
}
