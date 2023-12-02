using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem18;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem18", RowRegex());
        RunPartB("Problem18", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

