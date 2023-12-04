using AOCLib;
using AOCLib.Primitives;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem22;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        List<string> map = new List<string>();
        foreach (var data in datas)
        {
            var line = data.Value;
            if (String.IsNullOrWhiteSpace(line)) break;
            map.Add(line);
        }

        var instruction = datas.Last().Value;
        var instRegex = new Regex(@"(\d+)(R|L|$)");
        var instructions = instRegex.Matches(instruction).Select(r => (int.Parse(r.Groups[1].Value), r.Groups[2].Value)).ToList();

        var atMap = (Point p) =>
        {
            if (p.X < 1 || p.Y < 1 || p.Y > map.Count || p.X > map[(int)p.Y - 1].Length) return ' ';
            return map[(int)p.Y - 1][(int)p.X - 1];
        };

        var facing = 0; // right
        Point? point = null;
        for (var x = 0; x < map[0].Length; x++)
        {
            point = new Point (x + 1, 1);
            if (atMap(point) == '.')
            {
                break;
            }
        }

        if (point == null)
            throw new Exception();

        Bounds topFace;
        Bounds leftFace;
        Bounds rightFace;
        Bounds frontFace;
        Bounds backFace;
        Bounds bottomFace;
        if (InSample)
        {
            topFace = new Bounds(new Point(9, 1), new Point(12, 4));
            frontFace = new Bounds(new Point(9, 5), new Point(12, 8));
            leftFace = new Bounds(new Point(5, 5), new Point(8, 8));
            backFace = new Bounds(new Point(1, 5), new Point(4, 8));
            bottomFace = new Bounds(new Point(9, 9), new Point(12, 12));
            rightFace = new Bounds(new Point(13, 9), new Point(16, 12));
        }
        else
        {
            topFace = new Bounds(new Point(51, 1), new Point(100, 50));
            frontFace = new Bounds(new Point(51, 51), new Point(100, 100));
            leftFace = new Bounds(new Point(1, 101), new Point(50, 150));
            backFace = new Bounds(new Point(1, 151), new Point(50, 200));
            bottomFace = new Bounds(new Point(51, 101), new Point(100, 150));
            rightFace = new Bounds(new Point(101, 1), new Point(150, 50));
        }

        var area = new Area(topFace, frontFace, leftFace, backFace, bottomFace, rightFace);

        Point currentPoint = point;
        foreach (var ins in instructions)
        {
            for (int i = 0; i < ins.Item1; i++)
            {
                var stop = false;
                switch (facing)
                {
                    case 3:
                        {
                            Point newPoint = area.Up(currentPoint);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                            }
                            break;
                        }
                    case 0:
                        {
                            Point newPoint = area.Right(currentPoint);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                            }
                            break;
                        }
                    case 1:
                        {
                            Point newPoint = area.Down(currentPoint);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                            }
                            break;
                        }
                    case 2:
                        {
                            Point newPoint = area.Left(currentPoint);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                            }
                            break;
                        }
                    default: throw new Exception();
                }

                if (stop) break;
            }

            switch (ins.Value)
            {
                case "L":
                    {
                        facing--;
                        if (facing < 0) facing += 4;
                        break;
                    }
                case "R":
                    {
                        facing++;
                        if (facing > 3) facing -= 4;
                        break;
                    }
                default:
                    // final move may not contain a turn
                    break;
            }

            DebugLn($"M: {ins.Item1} D: {ins.Value} X: {currentPoint.X} Y: {currentPoint.Y} F: {facing} S: {currentPoint.Y * 1000 + currentPoint.X * 4 + facing}");
        }
        return (currentPoint.Y * 1000 + currentPoint.X * 4 + facing).ToString();
    }

}
