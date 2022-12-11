var commands = File.ReadAllLines("commands.txt").Select(Command.Parse);
var stacks1 = SetupStacks();
var stacks2 = SetupStacks();

foreach (var c in commands)
{
    c.Execute(stacks1);
    c.ExecuteMaintainingOrder(stacks2);
}

var result1 = string.Join("", stacks1.Select(x => x.Peek()));
var result2 = string.Join("", stacks2.Select(x => x.Peek()));

Console.WriteLine($"Result 1: {result1}");
Console.WriteLine($"Result 2: {result2}");

System.Diagnostics.Debug.Assert(result1 == "QNHWJVJZW");
System.Diagnostics.Debug.Assert(result2 == "BPCZJLFJW");

static List<Stack<char>> SetupStacks()
{
    var stackLines = File.ReadAllLines("stack.txt");
    var stacks = new List<Stack<char>>();

    for (var i = 0; i < stackLines[0].Length; i++)
        stacks.Add(new Stack<char>());

    foreach (var line in stackLines.Take(stackLines.Length - 1).Reverse())
    {
        for (var col = 0; col < line.Length; col++)
        {
            if (line[col] == ' ')
                continue;
            stacks[col].Push(line[col]);
        }
    }

    return stacks;
}

public readonly record struct Command(int Quantity, int From, int To)
{
    public static Command Parse(string line)
    {
        var values = line.Split(' ');
        return new Command(int.Parse(values[1]), int.Parse(values[3]), int.Parse(values[5]));
    }

    public void Execute(List<Stack<char>> stacks)
    {
        var fromStack = stacks[From - 1];
        var toStack = stacks[To - 1];

        for (var i = 0; i < Quantity; i++)
        {
            toStack.Push(fromStack.Pop());
        }
    }

    public void ExecuteMaintainingOrder(List<Stack<char>> stacks)
    {
        var fromStack = stacks[From - 1];
        var toStack = stacks[To - 1];
        var buffer = new List<char>();

        for (var i = 0; i < Quantity; i++)
            buffer.Add(fromStack.Pop());

        for (var i = buffer.Count - 1; i >= 0; i--)
            toStack.Push(buffer[i]);
    }
}