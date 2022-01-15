CSharp Itertools
================

[![downloads](https://img.shields.io/nuget/dt/CSharpItertools)](https://www.nuget.org/packages/CSharpItertools/)
[![latest version](https://img.shields.io/nuget/v/CSharpItertools)](https://www.nuget.org/packages/CSharpItertools/)

This library implements a number of iterator building blocks inspired by Python 3 itertools module. Each has been recast in a form suitable for C#.

## Installation

You can either register Itertools class in DI (Dependency Injection)
```csharp
services.AddItertools();
```

... or instantiate manually.
```csharp
IItertools itertools = new Itertools();
```

## Infinite iterators

### Cycle:
```csharp
using CSharpItertools;

var itertools = new Itertools();

foreach (char letter in itertools.Cycle("ABCD"))
    Console.WriteLine(letter);
```

Output:
```
A
B
C
D
A
B
... // repeats indefinitely
```

## Iterators terminating on the shortest input sequence

### Zip and ZipLongest:
Make an iterator that aggregates elements from each of the iterables.

```csharp
using CSharpItertools;

var itertools = new Itertools();

var iterable1 = new[] { 1, 2, 3 };
var iterable2 = new[] { 'A', 'B', 'C', 'D' };

forech (var item in itertools.Zip(iterable1, iterable2))
    Console.WriteLine(item);
  
forech (var item in itertools.ZipLongest(iterable1, iterable2))
    Console.WriteLine(item);
```

Output Zip:
```
(1, A)
(2, B)
(3, C)
```

Output ZipLongest:

- If the iterables are of **uneven length**, missing values are filled-in with `null`. Iteration continues until the longest iterable is exhausted.

```
(1, A)
(2, B)
(3, C)
(, D)
```

### FilterFalse:
Make an iterator that filters elements from iterable returning only those for which the predicate is `false`.

```csharp
using CSharpItertools;

var itertools = new Itertools();
var iterable = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

foreach (int i in itertools.FilterFalse(x => x % 2 > 0, iterable))
    Console.WriteLine(i);
```

Output:
```
2
4
6
8
```

### ISlice
Make an iterator that returns selected elements from the iterable.

```csharp
using CSharpItertools;

var itertools = new Itertools();
var iterable = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };

foreach (char l in itertools.ISlice(iterable, start: 3, stop: 6))
    Console.WriteLine(l);
```

Output:
```
D
E
F
```

### Compress
Make an iterator that filters elements from data returning **only** those that have a corresponding element in selectors that evaluates to `true`.

```csharp
using CSharpItertools;

var itertools = new Itertools();

var iterable = new[] { 'X', 'Y', 'Z', 'W' };
var selectors = new object[] { true, false, true, null };

foreach (char l in itertools.Compress(iterable, selectors))
    Console.WriteLine(l);
```

Output:
```
X
Z
```

## Combinatoric iterators

### Product
Cartesian product of input iterables.

```csharp
using CSharpItertools;

var itertools = new Itertools();

var iterable1 = new List<int> { 1, 2, 3 };
var iterable2 = new List<int> { 6, 7 };

foreach (var arr in itertools.Product(iterable1, iterable2))
    Console.WriteLine("[{0}, {1}]", arr[0], arr[1]); // Output 1
    
var iterable3 = new[] { 'X', 'Y', 'Z' };

foreach (var arr in itertools.Product(iterable3, repeat: 2))
    Console.WriteLine("[{0}, {1}]", arr[0], arr[1]); // Output 2
```

Output 1:
```
[1, 6]
[1, 7]
[2, 6]
[2, 7]
[3, 6]
[3, 7]
```

Output 2:
```
[X, X]
[X, Y]
[X, Z]
[Y, X]
[Y, Y]
[Y, Z]
[Z, X]
[Z, Y]
[Z, Z]
```

### Permutations
Return successive _r_ length permutations of elements in the iterable.

```csharp
using CSharpItertools;

var itertools = new Itertools();
var iterable = "ABCD";

foreach (var arr in itertools.Permutations(iterable, 2))
    Console.WriteLine("[{0}, {1}]", arr[0], arr[1]);
```

Output:
```
[A, B]
[A, C]
[A, D]
[B, A]
[B, C]
[B, D]
[C, A]
[C, B]
[C, D]
[D, A]
[D, B]
[D, C]
```

### Combinations
Return _r_ length subsequences of elements from the input iterable.

```csharp
using CSharpItertools;

var itertools = new Itertools();
var iterable = new object[] { 'A', 3, 9, 'Z' };

foreach (object[] arr in itertools.Combinations(iterable, 3))
    Console.WriteLine("[{0}, {1}, {2}]", arr[0], arr[1], arr[2]);
```

Output:
```
[A, 3, 9]
[A, 3, Z]
[A, 9, Z]
[3, 9, Z]
```
