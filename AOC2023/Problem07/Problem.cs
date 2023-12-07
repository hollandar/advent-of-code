using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem07;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem07", RowRegex());
        RunPartB("Problem07", RowRegex());
    }

    [GeneratedRegex("^(?<Cards>.*) (?<Bid>.*)$")]
    public static partial Regex RowRegex();
    
}

