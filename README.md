# csharp-itertools
Python itertools based C# library

### IZip

Make an iterator that aggregates elements from each of the iterables.

```cs
public class Program
{
	static Itertools _itertools = new Itertools();
	
	public static void Main()
	{
		var iterable1 = new char[] { 'A', 'B', 'C' };
		var iterable2 = new int[] { 1, 2, 3 };

		foreach (var item in _itertools.IZip(iterable1, iterable2))
			Console.WriteLine(item);
	}
}
```

Output:
```
(A, 1)
(B, 2)
(C, 3)
```

### IZipLongest

Make an iterator that aggregates elements from each of the iterables.
If the iterables are of uneven length, missing values are filled-in with *null*.

```cs
public class Program
{
	static Itertools _itertools = new Itertools();
	
	public static void Main()
	{
		var iterable1 = "CSharp";
		var iterable2 = new bool[] { true, true, false };

		foreach (var item in _itertools.IZipLongest(iterable1, iterable2))
			Console.WriteLine(item);
	}
}
```

Output:
```
(C, True)
(S, True)
(h, False)
(a, null)
(r, null)
(p, null)
```

### Combinations

Return r length subsequences of elements from the input iterable.

```cs
public class Program
{
	static Itertools _itertools = new Itertools();
	
	public static void Main()
	{
		var iterable1 = new string[] { "A", "B", "C", "D" };

		foreach (var item in _itertools.Combinations(iterable1, 2))
			Console.WriteLine($"({item.ElementAt(0)}, {item.ElementAt(1)})");
	}
}
```

Output:
```
(A, B)
(A, C)
(A, D)
(B, C)
(B, D)
(C, D)
```
