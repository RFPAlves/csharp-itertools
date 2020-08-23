# csharp-itertools
Python itertools based C# library

```cs
public void CombinationsOfElements()
{
    var iterable = new[] { "A", "B", "C" };

    var actual = new List<IEnumerable<string>>(itertools.Combinations(iterable, 2));

    var expected = new List<IEnumerable<string>>
    {
        new[] { "A", "B" },
        new[] { "A", "C" },
        new[] { "B", "C" }
    };

    Assert.Equal(actual, expected);
}
```