using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem16;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var valves = Load(datas);

        string startValveId = "AA";
        var startingValve = valves[startValveId];

        // Initial calculation didnt not accept the number of players, that was added for part 2.
        var answer = CalculateBest(startingValve, 30, 1);

        return answer.ToString();
    }
}
