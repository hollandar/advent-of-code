using AOCLib;
using AOCLib.Primitives;

namespace AdventOfCode2023.Problem03;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        CharacterGrid grid = new CharacterGrid(datas.Select(r => r.Value));

        var partNumbers = new List<Point>();
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                var p = new Point(x, y);
                if (grid[p] != '.' && !char.IsDigit(grid[p]))
                {
                    foreach (var adjacentPoint in p.AdjacentPoints(grid.Bounds))
                    {
                        if (char.IsDigit(grid[adjacentPoint]))
                            partNumbers.Add(adjacentPoint);
                    }
                }
            }
        }

        var coveredPoints = new HashSet<Point>();
        var parts = new List<string>();
        foreach (var point in partNumbers)
        {
            DebugLn(point.ToString());
            if (coveredPoints.Contains(point))
                continue;

            var startPoint = point;
            if (!Char.IsDigit(grid[startPoint]))
                throw new Exception($"Not a number at this location {startPoint}");
            while (startPoint.West().WithinBounds(grid.Bounds) && Char.IsDigit(grid[startPoint.West()]))
            {
                coveredPoints.Add(startPoint);
                startPoint = startPoint.West();
            }
            var endPoint = startPoint;
            while (endPoint.East().WithinBounds(grid.Bounds) && Char.IsDigit(grid[endPoint.East()]))
            {
                coveredPoints.Add(endPoint);
                endPoint = endPoint.East();
            }

            coveredPoints.Add(endPoint);

            var partNumber = grid.SubstringHorizontal(startPoint, startPoint.HorizontalLength(endPoint));
            DebugLn(partNumber);
            parts.Add(partNumber);

        }

        return parts.Select(p => int.Parse(p)).Sum().ToString();
    }

}
