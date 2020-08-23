using CSharpItertools.Interfaces;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Xunit;

namespace CSharpItertools.Tests
{
    public class ItertoolsTests
    {
        private readonly IItertools itertools = new Itertools();

        [Fact]
        public void IZipAggregateElementsSameLength()
        {
            var iterable1 = new[] { "A", "B", "C" };
            var iterable2 = new[] { 1, 2, 3 };

            var actual = new List<(string, int)>(itertools.IZip(iterable1, iterable2));

            var expected = new List<(string, int)>
            {
                ("A", 1),
                ("B", 2),
                ("C", 3)
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void IZipAggregateElementsUnevenLength()
        {
            var iterable1 = "Itertools";
            var iterable2 = new[] { true, false, false };

            var actual = new List<(char, bool)>(itertools.IZip(iterable1, iterable2));

            var expected = new List<(char, bool)>
            {
                ('I', true),
                ('t', false),
                ('e', false)
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void IZipAggregateElementsEmpty()
        {
            var iterable1 = new int[] { };
            var iterable2 = new int[] { };

            var actual = new List<(int, int)>(itertools.IZip(iterable1, iterable2));
            var expected = new List<(int, int)>();

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void IZipLongestAggregateElementsUnevenLength()
        {
            var iterable1 = new[] { 1.2, 0.98, 2.7, 8.45 };
            var iterable2 = new[] { 0x01, 0x02, 0x04 };

            var actual = new List<(object, object)>(itertools.IZipLongest(iterable1, iterable2));

            var expected = new List<(object, object)>
            {
                (1.2, 0x01),
                (0.98, 0x02),
                (2.7, 0x04),
                (8.45, null)
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CombinationsOfElements()
        {
            var iterable = new[] { "A", "B", "C" };

            var actual = new List<IEnumerable<string>>(itertools.Combinations(iterable, 2));

            var expected = new List<IEnumerable<string>>
            {
                new[] { "A", "B" },
                new[] { "A", "C" },
                new[] { "B", "C" }
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CombinationsOfDifferentsTypeOfElements()
        {
            var iterable = new object[] { 1, 'A', true, "Hi" };

            var actual = new List<IEnumerable<object>>(itertools.Combinations(iterable, 2));

            var expected = new List<IEnumerable<object>>
            {
                new object[] { 1, 'A' },
                new object[] { 1, true },
                new object[] { 1, "Hi" },
                new object[] { 'A', true },
                new object[] { 'A', "Hi" },
                new object[] { true, "Hi" }
            };

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void CombinationsLengthBiggerThanIterable()
        {
            var iterable = new[] { 'C' };

            var actual = new List<IEnumerable<char>>(itertools.Combinations(iterable, 2));
            var expected = new List<IEnumerable<char>>();

            Assert.Equal(actual, expected);
        }
    }
}
