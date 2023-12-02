using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem06;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem06", RowRegex());
        RunPartB("Problem06", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

