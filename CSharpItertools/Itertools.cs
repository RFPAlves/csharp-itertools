using CSharpItertools.Collections;
using CSharpItertools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpItertools
{
    public sealed class Itertools : IItertools
    {
        public IEnumerable<(T1, T2)> Zip<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2)
        {
            IEnumerator<T1> Enumerator1 = iterable1.GetEnumerator();
            IEnumerator<T2> Enumerator2 = iterable2.GetEnumerator();

            while (Enumerator1.MoveNext() && Enumerator2.MoveNext())
            {
                yield return (Enumerator1.Current, Enumerator2.Current);
            }
        }

        public IEnumerable<(object, object)> ZipLongest<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2)
        {
            IEnumerator<T1> Enumerator1 = iterable1.GetEnumerator();
            IEnumerator<T2> Enumerator2 = iterable2.GetEnumerator();

            while (true)
            {
                bool moveNext1 = Enumerator1.MoveNext();
                bool moveNext2 = Enumerator2.MoveNext();

                if (!moveNext1 && !moveNext2)
                    break;

                object output1 = moveNext1 ? Enumerator1.Current : (object)null;
                object output2 = moveNext2 ? Enumerator2.Current : (object)null;

                yield return (output1, output2);
            }
        }

        public IEnumerable<T> FilterFalse<T>(Predicate<T> predicate, IEnumerable<T> iterable)
        {
            foreach (var item in iterable)
                if (!predicate(item)) yield return item;
        }

        public IEnumerable<T> ISlice<T>(IEnumerable<T> iterable, int start, int? stop = null, int step = 1)
        {
            static IEnumerable<int> XRange(int startRange, int stopRange, int stepRange = 1)
            {
                for (; startRange < stopRange; startRange += stepRange)
                    yield return startRange;
            }

            stop ??= iterable.Count();

            IEnumerator<int> iterator = XRange(start, stop.Value, step).GetEnumerator();
            IEnumerable<(int, T)> iterableIndexed = Zip(Enumerable.Range(0, iterable.Count()), iterable);

            while (iterator.MoveNext())
            {
                foreach ((int Index, T Item) in iterableIndexed)
                {
                    if (iterator.Current == Index)
                        yield return Item;
                }
            }
        }

        public IEnumerable<T> Cycle<T>(IEnumerable<T> iterable)
        {
            ICollection<T> collection = new List<T>(iterable.Count());

            foreach (T element in iterable)
            {
                yield return element;
                collection.Add(element);
            }

            while (true)
            {
                foreach (T element in collection)
                    yield return element;
            }
        }

        public IEnumerable<T> Compress<T>(IEnumerable<T> iterable, IEnumerable<object> selectors)
        {
            foreach ((T Item, object Selector) in Zip(iterable, selectors))
            {
                if (Selector != null && Convert.ToBoolean(Selector.GetHashCode()))
                    yield return Item;
            }
        }

        public IEnumerable<T[]> Product<T>(params IEnumerable<T>[] iterables)
        {
            var result1 = new List<CustomArray<T>> { new CustomArray<T>() };
            var result2 = new List<CustomArray<T>>();

            foreach (var iterable in iterables)
            {
                foreach (var x in result1)
                    foreach (var y in iterable)
                        result2.Add(x + new CustomArray<T>(y));

                result1 = new List<CustomArray<T>>(result2);
                result2 = new List<CustomArray<T>>();
            }

            foreach (var r in result1)
                yield return r;
        }

        public IEnumerable<T[]> Product<T>(IEnumerable<T> iterable, int repeat)
        {
            var iterables = new IEnumerable<T>[repeat];

            for (int i = 0; i < repeat; i++)
                iterables[i] = new List<T>(iterable);

            foreach (T[] item in Product(iterables))
                yield return item;
        }

        public IEnumerable<T[]> Permutations<T>(IEnumerable<T> iterable, int r)
        {
            var newIterable = new List<T>(iterable);
            int count = newIterable.Count;

            foreach (int[] indices in Product(Enumerable.Range(0, count), repeat: r))
            {
                if (indices.Distinct().Count() == r)
                {
                    var result = new T[r];

                    for (int i = 0; i < indices.Length; i++)
                        result[i] = newIterable[indices[i]];

                    yield return result;
                }
            }
        }

        public IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> iterable, int r)
        {
            int n = iterable.Count();
            int i, breakWhile;

            if (r > n)
                yield break;

            int[] range = Enumerable.Range(0, r).ToArray();
            int[] reversedRange = range.Reverse().ToArray();

            IEnumerable<T> firstOutput, output;

            firstOutput = range.Select(x => iterable.ElementAt(x)).ToArray();

            yield return firstOutput;

            while (true)
            {
                i = 0;
                breakWhile = -1;

                for (; i < range.Length; i++)
                {
                    if (range.ElementAt(reversedRange.ElementAt(i)) != reversedRange.ElementAt(i) + n - r)
                    {
                        breakWhile++;
                        break;
                    }
                }

                if (breakWhile < 0) break;

                range[reversedRange.ElementAt(i)] += 1;

                foreach (int j in Enumerable.Range(reversedRange.ElementAt(i) + 1, r - (reversedRange.ElementAt(i) + 1)))
                    range[j] = range[j - 1] + 1;

                output = range.Select(x => iterable.ElementAt(x)).ToArray();

                yield return output;
            }
        }
    }
}