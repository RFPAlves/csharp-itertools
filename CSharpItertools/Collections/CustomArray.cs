using System.Collections.Generic;
using System.Collections;

namespace CSharpItertools.Collections
{
    internal sealed class CustomArray<T> : IEnumerable<T>, IEnumerable
    {
        private readonly T[] arr;

        public CustomArray() : this(0) { }

        public CustomArray(int size)
            => arr = new T[size];

        public CustomArray(params T[] items)
            => arr = items;

        public T this[int index]
        {
            get => arr[index];
            set => arr[index] = value;
        }

        public int Length => arr.Length;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in arr)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static CustomArray<T> operator +(CustomArray<T> customArrayFirst, CustomArray<T> customArraySecond)
        {
            var customArray = new CustomArray<T>(size: customArrayFirst.Length + customArraySecond.Length);

            int i = 0;

            for (int j = 0; j < customArrayFirst.Length; j++, i++)
                customArray[i] = customArrayFirst[j];

            for (int j = 0; j < customArraySecond.Length; j++, i++)
                customArray[i] = customArraySecond[j];

            return customArray;
        }

        public static implicit operator T[](CustomArray<T> a)
        {
            var arr = new T[a.Length];

            for (int i = 0; i < a.Length; i++)
                arr[i] = a[i];

            return arr;
        }
    }
}