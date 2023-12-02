using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem12;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem12", RowRegex());
        RunPartB("Problem12", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

