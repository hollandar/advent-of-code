using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem15;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem15", RowRegex());
        RunPartB("Problem15", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

