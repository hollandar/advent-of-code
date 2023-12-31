﻿using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem25;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem25", RowRegex());
        RunPartB("Problem25", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

