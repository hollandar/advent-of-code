using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem01;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem01", RowRegex());
        RunPartB("Problem01", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

