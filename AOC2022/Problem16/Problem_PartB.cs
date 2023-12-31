﻿using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem16;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var valves = Load(datas);

        string startValveId = "AA";
        var startingValve = valves[startValveId];

        var answer = CalculateBest(startingValve, 26, 2);

        return answer.ToString();

    }
}
