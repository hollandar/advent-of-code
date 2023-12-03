using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem17;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem17", RowRegex());
        RunPartB("Problem17", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

