using System.Diagnostics;

var part1 = Part1();
var part2 = Part2();

Console.WriteLine($"Part1: {part1}");
Console.WriteLine($"Part2: {part2}");

Debug.Assert(part1 == 1361);
Debug.Assert(part2 == 3263);

static int Part1()
{
    var input = File.ReadAllText("input.txt");
    return FindStarterPacketIndex(input, 4);
}

static int Part2()
{
    var input = File.ReadAllText("input.txt");
    return FindStarterPacketIndex(input, 14);
}

static int FindStarterPacketIndex(string input, int packetSize)
{
    var set = new HashSet<char>();

    for (var i = packetSize - 1; i < input.Length; i++)
    {
        foreach (var r in Enumerable.Range(0, packetSize))
            set.Add(input[i - r]);

        if (set.Count == packetSize)
            return i + 1;

        set.Clear();
    }
    return 0;
}