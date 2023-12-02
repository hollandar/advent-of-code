using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem10;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem10", RowRegex());
        RunPartB("Problem10", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

