using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem04;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem04", RowRegex());
        RunPartB("Problem04", RowRegex());
    }

    [GeneratedRegex("^(?<L1>\\d+)-(?<R1>\\d+),(?<L2>\\d+)-(?<R2>\\d+)$")]
    public static partial Regex RowRegex();
    
}

