using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem23;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem23", RowRegex());
        RunPartB("Problem23", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

