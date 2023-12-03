using AOCLib;
using System.Drawing;

namespace AdventOfCode2023.Problem03;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        List<char[]> rows = new List<char[]>();
        foreach (var data in datas)
        {
            rows.Add(data.Value.ToCharArray());
        }

        var partNumbers = new List<Point>();
        for (int x = 0; x < rows[0].Length; x++)
        {
            for (int y = 0; y < rows.Count; y++)
            {
                if (rows[y][x] != '.' && !char.IsDigit(rows[y][x]))
                {
                    if (x > 0)
                    {
                        if (y > 0 && char.IsDigit(rows[y - 1][x - 1]))
                        {
                            partNumbers.Add(new Point(x - 1, y - 1));
                        }
                        if (char.IsDigit(rows[y][x - 1]))
                        {
                            partNumbers.Add(new Point(x - 1, y));
                        }
                        if (y < rows.Count - 1 && char.IsDigit(rows[y + 1][x - 1]))
                        {
                            partNumbers.Add(new Point(x - 1, y + 1));
                        }
                    }
                    if (x < rows[y].Length - 1)
                    {
                        if (y > 0 && char.IsDigit(rows[y - 1][x + 1]))
                        {
                            partNumbers.Add(new Point(x + 1, y - 1));
                        }
                        if (char.IsDigit(rows[y][x + 1]))
                        {
                            partNumbers.Add(new Point(x + 1, y));
                        }
                        if (y < rows.Count - 1 && char.IsDigit(rows[y + 1][x + 1]))
                        {
                            partNumbers.Add(new Point(x + 1, y + 1));
                        }
                    }
                    if (y > 0)
                    {
                        if (char.IsDigit(rows[y - 1][x]))
                        {
                            partNumbers.Add(new Point(x, y - 1));
                        }
                    }

                    if (y < rows.Count - 1)
                    {
                        if (char.IsDigit(rows[y + 1][x]))
                        {
                            partNumbers.Add(new Point(x, y + 1));
                        }
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
            if (!Char.IsDigit(rows[startPoint.Y][startPoint.X]))
                throw new Exception($"Not a number at this location {startPoint}");
            while (startPoint.X > 0 && Char.IsDigit(rows[startPoint.Y][startPoint.X - 1]))
            {
                coveredPoints.Add(startPoint);
                startPoint = new Point(startPoint.X - 1, startPoint.Y);
            }
            var endPoint = startPoint;
            while (endPoint.X < rows[0].Length - 1 && Char.IsDigit(rows[endPoint.Y][endPoint.X + 1]))
            {
                coveredPoints.Add(endPoint);
                endPoint = new Point(endPoint.X + 1, endPoint.Y);
            }

            coveredPoints.Add(endPoint);

            var partNumber = new String(rows[startPoint.Y][startPoint.X..(endPoint.X + 1)]);
            DebugLn(partNumber);
            parts.Add(partNumber);

        }

        return parts.Select(p => int.Parse(p)).Sum().ToString();
    }

}
