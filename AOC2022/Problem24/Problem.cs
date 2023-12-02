using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem24;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem24", RowRegex());
        RunPartB("Problem24", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

