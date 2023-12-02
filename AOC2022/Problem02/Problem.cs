using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem02;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem02", RowRegex());
        RunPartB("Problem02", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

