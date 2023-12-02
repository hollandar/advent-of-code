using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem07;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem07", RowRegex());
        RunPartB("Problem07", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

