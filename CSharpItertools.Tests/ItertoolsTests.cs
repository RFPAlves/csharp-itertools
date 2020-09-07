using CSharpItertools.Interfaces;
using System;
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

            var actual = itertools.IZip(iterable1, iterable2);

            var expected = new List<(string, int)>
            {
                ("A", 1),
                ("B", 2),
                ("C", 3)
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IZipAggregateElementsUnevenLength()
        {
            var iterable1 = "Itertools";
            var iterable2 = new[] { true, false, false };

            var actual = itertools.IZip(iterable1, iterable2);

            var expected = new List<(char, bool)>
            {
                ('I', true),
                ('t', false),
                ('e', false)
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IZipAggregateElementsEmpty()
        {
            var iterable1 = new int[] { };
            var iterable2 = new int[] { };

            var actual = itertools.IZip(iterable1, iterable2);
            var expected = new List<(int, int)>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IZipLongestAggregateElementsUnevenLength()
        {
            var iterable1 = new[] { 1.2, 0.98, 2.7, 8.45 };
            var iterable2 = new[] { 0x01, 0x02, 0x04 };

            var actual = itertools.IZipLongest(iterable1, iterable2);

            var expected = new List<(object, object)>
            {
                (1.2, 0x01),
                (0.98, 0x02),
                (2.7, 0x04),
                (8.45, null)
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CombinationsOfElements()
        {
            var iterable = new[] { "A", "B", "C" };

            var actual = itertools.Combinations(iterable, 2);

            var expected = new List<IEnumerable<string>>
            {
                new[] { "A", "B" },
                new[] { "A", "C" },
                new[] { "B", "C" }
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CombinationsOfDifferentsTypeOfElements()
        {
            var iterable = new object[] { 1, 'A', true, "Hi" };

            var actual = itertools.Combinations(iterable, 2);

            var expected = new List<IEnumerable<object>>
            {
                new object[] { 1, 'A' },
                new object[] { 1, true },
                new object[] { 1, "Hi" },
                new object[] { 'A', true },
                new object[] { 'A', "Hi" },
                new object[] { true, "Hi" }
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CombinationsLengthBiggerThanIterable()
        {
            var iterable = new[] { 'C' };

            var actual = itertools.Combinations(iterable, 2);
            var expected = new List<IEnumerable<char>>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IFilterOddElements()
        {
            var iterable = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = itertools.IFilter(x => x % 2 > 0, iterable);
            var expected = new List<int> { 1, 3, 5, 7, 9 };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IFilterIterableElements()
        {
            var iterable = new object[] { 1, true, "IFilter" , 'C'};

            var actual = itertools.IFilter(x => x.GetType().GetInterface("IEnumerable") != null, iterable);
            var expected = new List<object> { "IFilter" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IFilterFalseOddElements()
        {
            var iterable = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = itertools.IFilterFalse(x => x % 2 > 0, iterable);
            var expected = new List<int> { 2, 4, 6, 8 };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ISliceElementsStep1()
        {
            var iterable = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };

            var actual = itertools.ISlice(iterable, start: 3, stop: 6);
            var expected = new List<char> { 'D', 'E', 'F' };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ISliceElementsStep2()
        {
            var iterable = new object[] { 'A', true, 0x04, "ISlice", 0.11, false, 'B' };

            var actual = itertools.ISlice(iterable, 0, step: 2);
            var expected = new List<object> { 'A', 0x04, 0.11, 'B' };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CycleElements()
        {
            var iterable = new[] { "A", "B", "C" };

            var actual = new List<string>(10);

            int i = 0;
            foreach (string element in itertools.Cycle(iterable))
            {
                actual.Add(element);
                if (++i >= 10) break;
            }

            var expected = new List<string> { "A", "B", "C", "A", "B", "C", "A", "B", "C", "A" };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CompressElementsWithNumericSelectors()
        {
            var iterable = new[] { 'X', 'Y', 'Z', 'W' };
            var selectors = new object[] { 1, 0, 1, 0 };

            var actual = itertools.Compress(iterable, selectors);
            var expected = new List<char> { 'X', 'Z' };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CompressElementsWithBooleanSelections()
        {
            var iterable = new[] { TimeSpan.FromDays(1), TimeSpan.FromDays(2), TimeSpan.FromDays(3), TimeSpan.FromDays(4) };
            var selectors = new object[] { true, false, true, null };

            var actual = itertools.Compress(iterable, selectors);
            var expected = new List<TimeSpan>
            {
                TimeSpan.FromDays(1),
                TimeSpan.FromDays(3)
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProductOfInputInterables()
        {
            var iterable1 = "ABC";
            var iterable2 = "123";

            var actual = itertools.Product(iterable1, iterable2);

            var expected = new List<char[]>
            {
                new char[] { 'A', '1' },
                new char[] { 'A', '2' },
                new char[] { 'A', '3' },
                new char[] { 'B', '1' },
                new char[] { 'B', '2' },
                new char[] { 'B', '3' },
                new char[] { 'C', '1' },
                new char[] { 'C', '2' },
                new char[] { 'C', '3' },
            };

            Assert.Equal(expected, actual);
        }
    }
}
