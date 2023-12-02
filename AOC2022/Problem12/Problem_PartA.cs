using AOCLib;
using AOCLib.AStar;

namespace AdventOfCode2022.Problem12;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);
        var grid = data.Grid;

        var path = PathFinder.FindPath(data.Start, data.End);

        var pathLength = path.NodeCount;
        PrintCells(data.Width, data.Height, data.Nodes, path);
        return pathLength.ToString();
    }

    void PrintCells(int w, int h, List<Node> nodes, DirectedPath path)
    {
        DebugLn("---");
        for (int y = 0; y < h; y++)
        {
            Debug(indent: 2);
            for (int x = 0; x < w; x++)
            {
                var node = nodes.Where(n => n.Point.X == x && n.Point.Y == y).First();
                var covered = path.GetEdges().Where(e =>
                    (e.Start.Point.X == x && e.Start.Point.Y == y) ||
                    (e.End.Point.X == x && e.End.Point.Y == y)
                ).Any();
                if (covered)
                    Debug("#");
                else
                    Debug(".");
            }

            DebugLn(indent: 0);
        }
    }
}
