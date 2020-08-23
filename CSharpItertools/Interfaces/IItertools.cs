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
    }
}