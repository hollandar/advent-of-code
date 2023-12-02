using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem04;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem04", RowRegex());
        RunPartB("Problem04", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

