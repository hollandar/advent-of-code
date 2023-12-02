using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem05;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem05", RowRegex());
        RunPartB("Problem05", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

