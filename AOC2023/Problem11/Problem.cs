using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem11", RowRegex());
        RunPartB("Problem11", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

