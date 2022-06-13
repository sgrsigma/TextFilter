using NUnit.Framework;
using System.Collections.Generic;
using TextFilter.App;
using TextFilter.App.Filters;
using TextFilter.App.Models;

namespace TextFilter.Tests
{
    public class LengthFilterTests
    {
        LengthFilter filter;

        [SetUp]
        public void Setup()
        {
            filter = new LengthFilter(3);
        }

        [Test]
        [TestCase("clean", ExpectedResult = "clean")]
        [TestCase("an", ExpectedResult = "")]
        [TestCase("and", ExpectedResult = "and")]
        public string ApplyLengthFilterLogicShould(string text)
        {
            return filter.ApplyFilterLogic(text);
        }


        [Test]
        public void LengthFilterShould()
        {
            var list = new List<Word>();
            list.Add(new Word("Ah", 0, 4, false));
            list.Add(new Word(" ", 4, 1, true));
            list.Add(new Word("rather", 5, 6, false));
            list.Add(new Word("!", 11, 1, true));
            list.Add(new Word("'", 12, 1, true));

            var expected = new List<Word>();
            expected.Add(new Word(string.Empty, 0, 4, false));
            expected.Add(new Word(" ", 4, 1, true));
            expected.Add(new Word("rather", 5, 6, false));
            expected.Add(new Word("!", 11, 1, true));
            expected.Add(new Word("'", 12, 1, true));

            var result = filter.Filter(list);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}