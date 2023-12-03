using AOCLib;
using AOCLib.Primitives;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;

namespace AdventOfCode2023.Problem03;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        CharacterGrid grid = new CharacterGrid(datas.Select(r => r.Value));

        var partNumbers = new List<(Point asteriskPoint, Point numberPoint)>();

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                var p = new Point(x, y);
                if (grid[p] == '*')
                {
                    foreach (var adjacentPoint in p.AdjacentPoints(grid.Bounds))
                    {
                        if (char.IsDigit(grid[adjacentPoint]))
                            partNumbers.Add((p, adjacentPoint));
                    }
                }
            }
        }

        var coveredPoints = new HashSet<Point>();
        var parts = new List<(Point asteriskPoint, string partNumber)>();
        foreach (var point in partNumbers)
        {
            DebugLn(point.ToString());
            if (coveredPoints.Contains(point.numberPoint))
                continue;

            var startPoint = point.numberPoint;
            if (!Char.IsDigit(grid[startPoint]))
                throw new Exception($"Not a number at this location {startPoint}");
            while (startPoint.Left().WithinBounds(grid.Bounds) && Char.IsDigit(grid[startPoint.Left()]))
            {
                coveredPoints.Add(startPoint);
                startPoint = startPoint.Left();
            }
            var endPoint = startPoint;
            while (endPoint.Right().WithinBounds(grid.Bounds) && Char.IsDigit(grid[endPoint.Right()]))
            {
                coveredPoints.Add(endPoint);
                endPoint = endPoint.Right();
            }

            coveredPoints.Add(endPoint);

            var partNumber = new String(grid.SubstringHorizontal(startPoint, startPoint.HorizontalLength(endPoint)));
            DebugLn(partNumber);
            parts.Add((point.asteriskPoint, partNumber));

        }

        var answer = 0;
        var groups = parts.GroupBy(r => r.asteriskPoint);
        foreach (var group in groups)
        {
            if (group.Count() != 2) continue;

            var part1 = group.First().partNumber;
            var part2 = group.Last().partNumber;
            var ratio = int.Parse(part1) * int.Parse(part2);
            answer += ratio;
        }

        return answer.ToString();
    }

}
