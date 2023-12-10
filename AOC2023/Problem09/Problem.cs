using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem09;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem09", RowRegex());
        RunPartB("Problem09", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

