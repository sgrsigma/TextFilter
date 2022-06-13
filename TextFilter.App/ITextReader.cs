using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.App
{
    public interface ITextReader
    {
        StreamReader StreamReader(string path);
    }
}
