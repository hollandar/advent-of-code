using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem03;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem03", RowRegex());
        RunPartB("Problem03", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

