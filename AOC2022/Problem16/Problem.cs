using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem16;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem16", RowRegex());
        RunPartB("Problem16", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

