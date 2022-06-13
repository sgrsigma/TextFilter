using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextFilter.App;
using TextFilter.App.Models;

namespace TextFilter.Tests
{
    public class TextProcessorTests
    {
        TextProcessor processor;
        string text = @"Alice was begining, and of having do:'Oh dear!' test!";
        
        [SetUp]
        public void Setup()
        {
            Mock<ITextReader> mockReader = new Mock<ITextReader>();
            
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(text);

            MemoryStream fakeStream = new MemoryStream(fakeFileBytes);
            mockReader.Setup(x => x.StreamReader(It.IsAny<string>()))
                .Returns(() => new StreamReader(fakeStream));

            processor = new TextProcessor(mockReader.Object);
        }

        [Test, Order(1)]
        public void TextProcessorReadShould()
        {
            processor.Read("text.txt");
            Assert.That(processor.OriginalText, Is.EqualTo(@"Alice was begining, and of having do:'Oh dear!' test!"));
        }

        [Test, Order(2)]
        public void TextProcessorExtractWordShould()
        {
            var result = new List<Word>();
            result.Add(new Word("Alice", 0, 5, false));
            result.Add(new Word(" ", 5, 1, true));
            result.Add(new Word("was", 6, 3, false));
            result.Add(new Word(" ", 9, 1, true));
            result.Add(new Word("begining", 10, 8, false));
            result.Add(new Word(",", 18, 1, true));
            result.Add(new Word(" ", 19, 1, true));
            result.Add(new Word("and", 20, 3, false));
            result.Add(new Word(" ", 23, 1, true));
            result.Add(new Word("of", 24, 2, false));
            result.Add(new Word(" ", 26, 1, true));
            result.Add(new Word("having", 27, 6, false));
            result.Add(new Word(" ", 33, 1, true));
            result.Add(new Word("do", 34, 2, false));
            result.Add(new Word(":", 36, 1, true));
            result.Add(new Word("'", 37, 1, true));
            result.Add(new Word("Oh", 38, 2, false));
            result.Add(new Word(" ", 40, 1, true));
            result.Add(new Word("dear", 41, 4, false));
            result.Add(new Word("!", 45, 1, true));
            result.Add(new Word("'", 46, 1, true));
            result.Add(new Word(" ", 47, 1, true));
            result.Add(new Word("test", 48, 4, false));
            result.Add(new Word("!", 52, 1, true));

            processor.Read("test.txt");
            processor.ExtractWords();

            for(int i = 0; i < 24; i++)
            {
                var obj1 = processor.Words[i];
                var obj2 = result[i];
                Assert.AreEqual(obj1, obj2);
            }

            CollectionAssert.AreEqual(processor.Words, result);
        }

        [Test]
        public void TextProcessorExtractWordEmptyShould()
        {
            var result = new List<Word>();
            processor.ExtractWords();
            CollectionAssert.AreEqual(processor.Words, result);
        }

        [Test, Order(3)]
        public void TextProcessorFilterShould()
        {
            var result = new List<Word>();
            result.Add(new Word(string.Empty, 0, 5, false));
            result.Add(new Word(" ", 5, 1, true));
            result.Add(new Word(string.Empty, 6, 3, false));
            result.Add(new Word(" ", 9, 1, true));
            result.Add(new Word(string.Empty, 10, 8, false));
            result.Add(new Word(",", 18, 1, true));
            result.Add(new Word(" ", 19, 1, true));
            result.Add(new Word("and", 20, 3, false));
            result.Add(new Word(" ", 23, 1, true));
            result.Add(new Word(string.Empty, 24, 2, false));
            result.Add(new Word(" ", 26, 1, true));
            result.Add(new Word(string.Empty, 27, 6, false));
            result.Add(new Word(" ", 33, 1, true));
            result.Add(new Word(string.Empty, 34, 2, false));
            result.Add(new Word(":", 36, 1, true));
            result.Add(new Word("'", 37, 1, true));
            result.Add(new Word(string.Empty, 38, 2, false));
            result.Add(new Word(" ", 40, 1, true));
            result.Add(new Word(string.Empty, 41, 4, false));
            result.Add(new Word("!", 45, 1, true));
            result.Add(new Word("'", 46, 1, true));
            result.Add(new Word(" ", 47, 1, true));
            result.Add(new Word(string.Empty, 48, 4, false));
            result.Add(new Word("!", 52, 1, true));

            processor.Read("test.txt");
            processor.ExtractWords();
            processor.Filter();

            CollectionAssert.AreEqual(result, processor.Words);
        }

        [Test]
        public void TextProcessorFilterEmptyShould()
        {
            var result = new List<Word>();

            processor.ExtractWords();
            processor.Filter();

            CollectionAssert.AreEqual(result, processor.Words);
        }

        [Test, Order(4)]
        public void TextProcessorMergeShould()
        {
            processor.Read("test.txt");
            processor.ExtractWords();
            processor.Filter();
            processor.Merge();

            Console.WriteLine(processor.FilteredText);

            Assert.IsTrue(processor.FilteredText == "  , and   :' !' !");
        }

        [Test]
        public void TextProcessorMergeEmptyShould()
        {
            processor.ExtractWords();
            processor.Filter();
            processor.Merge();

            Assert.IsTrue(processor.FilteredText == "");
        }
    }
}
