var input = File
    .ReadAllLines("input.txt")
    .Select(x => x.Split(new char[] { '-', ',' }))
    .Select(x =>
        (new Range(int.Parse(x[0]), int.Parse(x[1])),
        new Range(int.Parse(x[2]), int.Parse(x[3]))));


var result1 = input.Count(x => x.Item1.ContainsEachOther(x.Item2));
var result2 = input.Count(x => x.Item1.Overlap(x.Item2));

Console.WriteLine($"Result 1: {result1}");
Console.WriteLine($"Result 2: {result2}");

System.Diagnostics.Debug.Assert(result1 == 485);
System.Diagnostics.Debug.Assert(result2 == 857);

public readonly record struct Range(int Lower, int Upper)
{
    public bool ContainsEachOther(Range other) => Contains(other) || other.Contains(this);
    public bool Contains(Range other) => Upper >= other.Upper && Lower <= other.Lower;
    public bool Overlap(Range other)
    {
        return (Lower >= other.Lower && Lower <= other.Upper)
            || (Upper >= other.Lower && Upper <= other.Upper)
            || ContainsEachOther(other);
    }
}