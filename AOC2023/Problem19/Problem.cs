using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem19;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem19", RowRegex());
        RunPartB("Problem19", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

