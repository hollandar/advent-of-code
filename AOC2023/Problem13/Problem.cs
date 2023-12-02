using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem13;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem13", RowRegex());
        RunPartB("Problem13", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

