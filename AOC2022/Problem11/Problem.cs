using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem11", RowRegex());
        RunPartB("Problem11", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

