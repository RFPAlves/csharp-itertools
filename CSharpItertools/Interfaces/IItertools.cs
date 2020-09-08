using System;
using System.Collections.Generic;

namespace CSharpItertools.Interfaces
{
    public interface IItertools
    {
        /// <summary>
        /// Make an iterator that aggregates elements from each of the iterables.
        /// </summary>
        IEnumerable<(T1, T2)> Zip<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2);
        
        /// <summary>
        /// Make an iterator that aggregates elements from each of the iterables.
        /// If the iterables are of uneven length, missing values are filled-in with null.
        /// </summary>
        IEnumerable<(object, object)> ZipLongest<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2);
        
        /// <summary>
        /// Make an iterator that filters elements from iterable returning only
        /// those for which the predicate is false.
        /// </summary>
        IEnumerable<T> FilterFalse<T>(Predicate<T> predicate, IEnumerable<T> iterable);

        /// <summary>
        /// Make an iterator that returns selected elements from the iterable.
        /// </summary>
        IEnumerable<T> ISlice<T>(IEnumerable<T> iterable, int start, int? stop = null, int step = 1);

        /// <summary>
        /// Make an iterator returning repeated elements indefinitely.
        /// </summary>
        IEnumerable<T> Cycle<T>(IEnumerable<T> iterable);

        /// <summary>
        /// Make an iterator that filters elements from data returning only those that
        /// have a corresponding element in selectors that evaluates to True.
        /// </summary>
        IEnumerable<T> Compress<T>(IEnumerable<T> iterable, IEnumerable<object> selectors);

        /// <summary>
        /// Cartesian product of input iterables.
        /// </summary>
        IEnumerable<T[]> Product<T>(params IEnumerable<T>[] iterables);

        /// <summary>
        /// Cartesian product of input iterables.
        /// </summary>
        IEnumerable<T[]> Product<T>(IEnumerable<T> iterable, int repeat);

        /// <summary>
        /// Return successive r length permutations of elements in the iterable.
        /// </summary>
        IEnumerable<T[]> Permutations<T>(IEnumerable<T> iterable, int r);

        /// <summary>
        /// Return r length subsequences of elements from the input iterable
        /// </summary>
        IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> iterable, int r);
    }
}