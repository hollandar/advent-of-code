using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem14;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem14", RowRegex());
        RunPartB("Problem14", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    class Cell
    {
        public bool sand;
        public bool rock;

        public Cell()
        {
            sand = false;
            rock = false;
        }
    }
}

