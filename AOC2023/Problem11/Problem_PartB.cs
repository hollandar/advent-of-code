using AOCLib;
using System.Numerics;

namespace AdventOfCode2023.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        SetDebug();
        BigInteger answer = 0;
        foreach (var data in datas)
        {
            long rowVariations = 1;
            var springs = data.Springs;
            var brokenGroups = data.Numbers.Split(',').Select(x => int.Parse(x)).ToArray();

            ulong v1 = Match( springs, brokenGroups);
            ulong v2 = Match(springs + "?" + springs, [..brokenGroups, ..brokenGroups]);

            ulong x = v2/v1;
            ulong a = (v2 * x * x * x);
            DebugLn($"RESULT: {springs} {v1} {v2} {a}");

            answer += a;
        }

        return answer.ToString();
    }
}


