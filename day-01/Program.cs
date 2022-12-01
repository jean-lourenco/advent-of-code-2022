var nl = Environment.NewLine;
var calories = File.ReadAllText("input.txt")
    .Split(nl + nl)
    .Select(x => x.Split(nl).Select(int.Parse).Sum())
    .OrderByDescending(x => x)
    .Take(3)
    .ToArray();

Console.WriteLine($"Result 1: {calories.First()}");
Console.WriteLine($"Result 2: {calories.Sum()}");

System.Diagnostics.Debug.Assert(calories.First() == 67633);
System.Diagnostics.Debug.Assert(calories.Sum() == 199628);