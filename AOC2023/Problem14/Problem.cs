using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem14;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem14", RowRegex());
        RunPartB("Problem14", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

