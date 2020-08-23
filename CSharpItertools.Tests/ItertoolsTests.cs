using CSharpItertools.Interfaces;
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
    }
}
