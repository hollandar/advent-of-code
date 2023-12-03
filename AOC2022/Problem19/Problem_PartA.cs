using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem19;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var blueprints = Load(datas);

        int totalQuality = 0;
        for (int i = 0; i < blueprints.Count; i++)
        {
            var optimalProduction = Solve(blueprints[i], new Inventory { oreRobots = 1, minutesLeft = 24 });
            DebugLn($"{blueprints[i].id}: {optimalProduction}");
            int quality = blueprints[i].id * optimalProduction;
            totalQuality += quality;
        }

        return totalQuality.ToString();
    }

}
