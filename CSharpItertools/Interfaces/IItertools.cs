using System;
using System.Collections.Generic;

namespace CSharpItertools.Interfaces
{
    public interface IItertools
    {
        /// <summary>
        /// Make an iterator that aggregates elements from each of the iterables.
        /// </summary>
        /// <param name="iterable1">First iterable</param>
        /// <param name="iterable2">Second iterable</param>
        /// <returns>An iterator with aggregate elements</returns>
        IEnumerable<(T1, T2)> IZip<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2);
        
        /// <summary>
        /// Make an iterator that aggregates elements from each of the iterables.
        /// If the iterables are of uneven length, missing values are filled-in with null.
        /// </summary>
        /// <param name="iterable1">First iterable</param>
        /// <param name="iterable2">Second iterable</param>
        /// <returns>An iterator with aggregate elements</returns>
        IEnumerable<(object, object)> IZipLongest<T1, T2>(IEnumerable<T1> iterable1, IEnumerable<T2> iterable2);
        
        /// <summary>
        /// Return r length subsequences of elements from the input iterable
        /// </summary>
        /// <param name="items">Input iterable</param>
        /// <param name="r">Length subsequences of elements</param>
        /// <returns>An iterator with combined elements</returns>
        IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> iterable, int r);

        /// <summary>
        /// Make an iterator that filters elements from iterable returning only
        /// those for which the predicate is true.
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <param name="iterable">Iterable that will be filtered</param>
        /// <returns>An iterator with filtered values</returns>
        IEnumerable<T> IFilter<T>(Predicate<T> predicate, IEnumerable<T> iterable);

        /// <summary>
        /// Make an iterator that filters elements from iterable returning only
        /// those for which the predicate is false.
        /// </summary>
        /// <param name="predicate">Predicate function</param>
        /// <param name="iterable">Iterable that will be filtered</param>
        /// <returns>An iterator with filtered values</returns>
        IEnumerable<T> IFilterFalse<T>(Predicate<T> predicate, IEnumerable<T> iterable);

        /// <summary>
        /// Make an iterator that returns selected elements from the iterable.
        /// </summary>
        /// <param name="iterable">Iterable that will be sliced</param>
        /// <param name="start">Start at the specified position</param>
        /// <param name="stop">Stop at the specified position</param>
        /// <param name="step">Slice steps</param>
        /// <returns>An iterator with selected values</returns>
        IEnumerable<T> ISlice<T>(IEnumerable<T> iterable, int start, int? stop = null, int step = 1);
    }
}