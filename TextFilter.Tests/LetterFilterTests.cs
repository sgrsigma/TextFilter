using NUnit.Framework;
using System.Collections.Generic;
using TextFilter.App;
using TextFilter.App.Filters;
using TextFilter.App.Models;

namespace TextFilter.Tests
{
    [TestFixture]
    public class LetterFilterTests
    {
        LetterFilter filter;

        [SetUp]
        public void Setup()
        {
            filter = new LetterFilter('t');
        }

        [Test]
        [TestCase("clean", ExpectedResult = "clean")]
        [TestCase("three", ExpectedResult = "")]
        [TestCase("Three", ExpectedResult = "")]
        [TestCase("the", ExpectedResult = "")]
        public string ApplyLetterLogicFilterShould(string text)
        {
            return filter.ApplyFilterLogic(text);
        }

        [Test]
        public void LetterFilterShould()
        {
            var list = new List<Word>();
            list.Add(new Word("Clean", 0, 4, false));
            list.Add(new Word(" ", 4, 1, true));
            list.Add(new Word("rather", 5, 6, false));
            list.Add(new Word("!", 11, 1, true));
            list.Add(new Word("'", 12, 1, true));

            var expected = new List<Word>();
            expected.Add(new Word("Clean", 0, 4, false));
            expected.Add(new Word(" ", 4, 1, true));
            expected.Add(new Word(string.Empty, 5, 6, false));
            expected.Add(new Word("!", 11, 1, true));
            expected.Add(new Word("'", 12, 1, true));

            var result = filter.Filter(list);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
