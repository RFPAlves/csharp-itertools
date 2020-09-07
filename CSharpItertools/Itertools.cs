﻿using CSharpItertools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSharpItertools
{
    public class Itertools : IItertools
    {
        public IEnumerable<(T1, T2)> IZip<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2)
        {
            IEnumerator<T1> Enumerator1 = iterable1.GetEnumerator();
            IEnumerator<T2> Enumerator2 = iterable2.GetEnumerator();

            while (Enumerator1.MoveNext() && Enumerator2.MoveNext())
            {
                yield return (Enumerator1.Current, Enumerator2.Current);
            }
        }

        public IEnumerable<(object, object)> IZipLongest<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2)
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

        public IEnumerable<T> IFilter<T>(Predicate<T> predicate, IEnumerable<T> iterable)
        {
            foreach (var item in iterable)
                if (predicate(item)) yield return item;
        }

        public IEnumerable<T> IFilterFalse<T>(Predicate<T> predicate, IEnumerable<T> iterable)
        {
            foreach (var item in iterable)
                if (!predicate(item)) yield return item;
        }

        public IEnumerable<T> ISlice<T>(IEnumerable<T> iterable, int start, int? stop = null, int step = 1)
        {
            IEnumerable<int> XRange(int startRange, int stopRange, int stepRange = 1)
            {
                for (; startRange < stopRange; startRange += stepRange)
                    yield return startRange;
            }

            stop = stop ?? iterable.Count();

            IEnumerator<int> iterator = XRange(start, stop.Value, step).GetEnumerator();
            IEnumerable<(int, T)> iterableIndexed = IZip(Enumerable.Range(0, iterable.Count()), iterable);

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
            foreach ((T Item, object Selector) in IZip(iterable, selectors))
            {
                if (Selector != null && Convert.ToBoolean(Selector.GetHashCode()))
                    yield return Item;
            }
        }
    }
}