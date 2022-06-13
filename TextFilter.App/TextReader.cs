using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.App
{
    public class TextReader : ITextReader
    {
        public StreamReader StreamReader(string path)
        {
            var reader = new StreamReader(path);
            return reader;
        }
    }
}
