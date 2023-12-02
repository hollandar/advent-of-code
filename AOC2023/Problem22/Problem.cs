using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem22;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem22", RowRegex());
        RunPartB("Problem22", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

