﻿using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem20;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem20", RowRegex());
        RunPartB("Problem20", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
}

