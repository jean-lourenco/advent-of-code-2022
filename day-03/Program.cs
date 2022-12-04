var input = File.ReadAllLines("input.txt");
// var input = File.ReadAllLines("sample.txt");

var items = new HashSet<char>();
var duplicated = new HashSet<char>();
var result1 = 0;

foreach (var line in input)
{
    var itemsPerSide = line.Length / 2;
    var firstSide = line.Substring(0, itemsPerSide);
    var secondSide = line.Substring(itemsPerSide);

    foreach (var item in firstSide)
        items.Add(item);

    foreach (var item in secondSide)
    {
        if (items.Contains(item))
            duplicated.Add(item);
    }

    result1 += duplicated.Sum(ASCIIToElfNumber);
    items.Clear();
    duplicated.Clear();
}

var dict = new Dictionary<char, int>();
var setted = new HashSet<char>();
var result2 = 0;
foreach (var chunk in input.Chunk(3))
{
    var i = 1;
    foreach (var line in chunk)
    {
        foreach (var l in line)
        {
            if (setted.Contains(l))
                continue;

            setted.Add(l);

            if (!dict.ContainsKey(l))
                dict.Add(l, 1);
            else
                dict[l]++;
        }
        i++;
        setted.Clear();
    }

    result2 += ASCIIToElfNumber(dict.Single(x => x.Value == 3).Key);
    dict.Clear();
}


Console.WriteLine($"Result 1: {result1}");
System.Diagnostics.Debug.Assert(result1 == 7785);

Console.WriteLine($"Result 2: {result2}");
System.Diagnostics.Debug.Assert(result2 == 2633);

static int ASCIIToElfNumber(char c) => c < 96 ? c - 38 : c - 96;