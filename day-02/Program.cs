var firstScore = 0;
var secondScore = 0;

var input = File.ReadAllLines("input.txt");
foreach (var line in input)
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    var o = line[0];
    var p = line[2];
    char winningPlay;

    if ((o == 'A' && p == 'X') || (o == 'B' && p == 'Y') || (o == 'C' && p == 'Z'))
        firstScore += 3;
    else if ((o == 'A' && p == 'Y') || (o == 'B' && p == 'Z') || (o == 'C' && p == 'X'))
        firstScore += 6;

    if ((o == 'A' && p == 'X') || (o == 'C' && p == 'Y') || (o == 'B' && p == 'Z'))
        winningPlay = 'Z';
    else if ((o == 'C' && p == 'X') || (o == 'B' && p == 'Y') || (o == 'A' && p == 'Z'))
        winningPlay = 'Y';
    else
        winningPlay = 'X';

    firstScore += p == 'X' ? 1 : p == 'Y' ? 2 : 3;

    secondScore += p == 'Z' ? 6 : p == 'Y' ? 3 : 0;
    secondScore += winningPlay == 'X' ? 1 : winningPlay == 'Y' ? 2 : 3;
}

Console.WriteLine($"Result 1: {firstScore}");
Console.WriteLine($"Result 2: {secondScore}");
System.Diagnostics.Debug.Assert(firstScore == 15572);
System.Diagnostics.Debug.Assert(secondScore == 16098);