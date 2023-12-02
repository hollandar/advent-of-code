using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem08;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem08", RowRegex());
        RunPartB("Problem08", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

