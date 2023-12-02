using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.ProblemT;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("ProblemT", RowRegex());
        RunPartB("ProblemT", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

