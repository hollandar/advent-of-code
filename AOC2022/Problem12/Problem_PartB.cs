using AOCLib;
using AOCLib.AStar;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem12;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        int minimumPathLength = int.MaxValue;
        foreach (var low in data.Lowest)
        {

            var path = PathFinder.FindPath(low, data.End);
            if (!path.Incomplete)
            {
                var pathLength = path.NodeCount;
                if (pathLength < minimumPathLength)
                {
                    minimumPathLength = pathLength;
                    PrintCells(data.Width, data.Height, data.Nodes, path);
                }
            }
        }

        return minimumPathLength.ToString();
    }

}
