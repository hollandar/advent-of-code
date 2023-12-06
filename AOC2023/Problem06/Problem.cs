using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem06;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem06", RowRegex());
        RunPartB("Problem06", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

