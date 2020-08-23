using CSharpItertools.Interfaces;
using System.Collections.Generic;

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
	}
}
