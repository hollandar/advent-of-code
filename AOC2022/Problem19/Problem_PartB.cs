using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem19;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var blueprints = Load(datas).Take(3).ToList();

        int totalQuality = 0;
        List<int> results = new List<int>();

        for (int i = 0; i < blueprints.Count; i++)
        {
            var optimalProduction = Solve(blueprints[i], new Inventory { oreRobots = 1, minutesLeft = 32 });
            DebugLn($"{blueprints[i].id}: {optimalProduction}");
            results.Add(optimalProduction);
        }

        return results.OrderByDescending(r => r).Aggregate((a, b) => a * b).ToString();
    }

}
