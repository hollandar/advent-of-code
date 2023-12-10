using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem10;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem10", RowRegex());
        RunPartB("Problem10", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    char[] hasNorthConnection = ['|', 'L', 'J', 'S'];
    char[] hasSouthConnection = ['|', '7', 'F', 'S'];
    char[] hasEastConnection = ['-', 'L', 'F', 'S'];
    char[] hasWestConnection = ['-', 'J', '7', 'S'];

    enum Direction
    {
        North,
        South,
        East,
        West
    }

    bool Connected(char position, char otherPosition, Direction direction)
    {
        return direction switch
        {
            Direction.North => hasNorthConnection.Contains(position) && hasSouthConnection.Contains(otherPosition),
            Direction.South => hasSouthConnection.Contains(position) && hasNorthConnection.Contains(otherPosition),
            Direction.East => hasEastConnection.Contains(position) && hasWestConnection.Contains(otherPosition),
            Direction.West => hasWestConnection.Contains(position) && hasEastConnection.Contains(otherPosition),
        };
    }
}

