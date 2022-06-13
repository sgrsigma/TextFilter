using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextFilter.App
{
    class Program
    {
        static void Main(string[] args)
        {
            ITextReader reader = new TextReader();

            var processor = new TextProcessor(reader);

            try
            {
                Console.WriteLine($"Read file...");
                processor.Read(@"example.txt");

                Console.WriteLine($"Extract words...");
                processor.ExtractWords();

                Console.WriteLine($"Filter...");
                processor.Filter();
                processor.Merge();
                Console.WriteLine($"-------Result----------");

                Console.WriteLine(processor.FilteredText);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Process Error: {ex.Message}");
            }
            finally
            {
                processor.Dispose();
            }

            Console.ReadKey();
        }
    }
}
