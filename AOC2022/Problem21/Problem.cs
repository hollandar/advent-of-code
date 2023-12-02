using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem21;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem21", RowRegex());
        RunPartB("Problem21", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

