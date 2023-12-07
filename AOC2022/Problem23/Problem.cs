using AOCLib;
using AOCLib.Primitives;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem23;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem23", RowRegex());
        RunPartB("Problem23", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    class Elf
    {
        public Point position;
        public Point proposal;
        public bool valid_proposal;
    }

    struct Cell
    {
        public short elves;
        public short proposals;
    }

    void PrintArea(Bounds mapSize, Point center, Cell[,] area)
    {
        for (long y = mapSize.TopLeft.Y; y <= mapSize.BottomRight.Y; y++)
        {
            for (long x = mapSize.TopLeft.X; x <= mapSize.BottomRight.X; x++)
            {
                if (area[(int)(y + center.Y), (int)(x + center.X)].elves == 1) Debug("#");
                else Debug(".");
            }
            DebugLn();
        }
        DebugLn();
    }
}

