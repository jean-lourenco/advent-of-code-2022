using System.Diagnostics;

var input = System.IO.File.ReadAllLines("input.txt");
var root = new Directory("/", null);
var curDir = root;

for (var i = 0; i < input.Length; i++)
{
    var c = input[i];

    if (c.StartsWith("$ cd"))
    {
        var target = c[5..];
        if (target == "..")
        {
            curDir = curDir.Parent ?? curDir;
            continue;
        }
        else if (target == "/")
        {
            curDir = root;
            continue;
        }

        curDir = (Directory)curDir.Items[target];
    }
    else if (c == "$ ls")
    {
        while (i + 1 < input.Length && !input[i + 1].StartsWith('$'))
        {
            var value = input[++i].Split(' ');
            if (value[0] == "dir")
                curDir.Items.TryAdd(value[1], new Directory(value[1], curDir));
            else
                curDir.Items.TryAdd(value[1], new File(value[1], int.Parse(value[0])));
        }
    }
}

var set = new HashSet<Directory>();
var part1 = GetDirectories(root, set, x => x <= 100_000).Sum(x => x.Size);
System.Console.WriteLine(part1);
Debug.Assert(part1 == 1490523);

const int freeSpace = 70_000_000;
const int updateSpace = 30_000_000;

set.Clear();
GetDirectories(root, set, x => true);
var spaceRequired = Math.Abs(freeSpace - root.Size - updateSpace);

var part2 = set.OrderBy(x => x.Size).First(x => x.Size > spaceRequired).Size;
System.Console.WriteLine(part2);
Debug.Assert(part2 == 12390492);

static HashSet<Directory> GetDirectories(Directory cur, HashSet<Directory> set, Func<int, bool> filter)
{
    if (filter(cur.Size))
        set.Add(cur);

    foreach (var d in cur.Items.Values.Where(x => x is Directory))
        GetDirectories((Directory)d, set, filter);

    return set;
}

public interface ISize { int Size { get; } string Name { get; } }
public readonly record struct File(string Name, int Size) : ISize;
public record Directory(string Name, Directory? Parent) : ISize
{
    public Dictionary<string, ISize> Items { get; } = new Dictionary<string, ISize>();
    public int Size => Items.Values.Sum(x => x.Size);
}