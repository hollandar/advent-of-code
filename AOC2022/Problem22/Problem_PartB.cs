using AOCLib;
using AOCLib.Primitives;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem22;

public partial class Problem : ProblemPart<InputRow>
{

    // This component is mapped by hand for the Input, rather than the sample.
    // The sample will return the wrong number.

    protected override string PartB(IEnumerable<InputRow> datas)
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
            point = new Point(x + 1, 1);
            if (atMap(point) == '.')
            {
                break;
            }
        }

        if (point == null)
            throw new Exception();

        var right = (Point p, int facing) =>
        {
            var right = new Point(p.X + 1, p.Y);

            if (right.X == 151 && right.Y >= 1 && right.Y <= 50)
            {
                right = new Point(100, 151 - right.Y);
                facing = 2;
            }

            if (right.X == 101 && right.Y >= 51 && right.Y <= 100)
            {
                right = new Point(right.Y + 50, 50);
                facing = 3;
            }

            if (right.X == 101 && right.Y >= 101 && right.Y <= 150)
            {
                right = new Point(150, 50 - (right.Y - 101));
                facing = 2;
            }

            if (right.X == 51 && right.Y >= 151 && right.Y <= 200)
            {
                right = new Point(right.Y - 100, 150);
                facing = 3;
            }

            //if (atMap(right) == ' ')
            //    right = new Point { x = 1, y = p.y };

            //while (atMap(right) == ' ')
            //{
            //    right = new Point { x = right.x + 1, y = p.y };
            //}

            return (right, facing);
        };

        var left = (Point p, int facing) =>
        {
            var left = new Point(p.X - 1, p.Y);

            if (left.X == 50 && left.Y >= 1 && left.Y <= 50)
            {
                left = new Point(1, 151 - left.Y);
                facing = 0;
            }
            if (left.X == 50 && left.Y >= 51 && left.Y <= 100)
            {
                left = new Point(left.Y - 50, 101);
                facing = 1;
            }
            if (left.X == 0 && left.Y >= 101 && left.Y <= 150)
            {
                left = new Point(51, 50 - (left.Y - 101));
                facing = 0;
            }
            if (left.X == 0 && left.Y >= 151 && left.Y <= 200)
            {
                left = new Point(left.Y - 100, 1);
                facing = 1;
            }

            //if (atMap(left) == ' ')
            //    left = new Point { x = map[p.y - 1].Length, y = p.y };

            //while (atMap(left) == ' ')
            //{
            //    left = new Point { x = left.x - 1, y = p.y };
            //}

            return (left, facing);
        };

        var up = (Point p, int facing) =>
        {
            var up = new Point(p.X, p.Y - 1);
            if (up.Y == 0 && up.X >= 51 && up.X <= 100) //1
            {
                up = new Point(1, up.X + 100);
                facing = 0;
            }

            if (up.Y == 0 && up.X >= 101 && up.X <= 150)
            {
                up = new Point(up.X - 100, 200);
                facing = 3;
            }

            if (up.Y == 100 && up.X >= 1 && up.X <= 50)
            {
                up = new Point(51, up.X + 50);
                facing = 0;
            }
            //if (atMap(up) == ' ')
            //    up = new Point { x = p.x, y = map.Count };

            //while (atMap(up) == ' ')
            //{
            //    up = new Point { x = p.x, y = up.y - 1 };
            //}

            return (up, facing);
        };

        var down = (Point p, int facing) =>
        {
            var down = new Point(p.X, p.Y + 1);
            if (down.Y == 51 && down.X >= 101 && down.X <= 150)
            {
                down = new Point(100, down.X - 50);
                facing = 2;
            }

            if (down.Y == 151 && down.X >= 51 && down.X <= 100)
            {
                down = new Point(50, down.X + 100);
                facing = 2;
            }

            if (down.Y == 201 && down.X >= 1 && down.X <= 50)
            {
                down = new Point(down.X + 100, 1);
                facing = 1;
            }

            //if (atMap(down) == ' ')
            //    down = new Point { x = p.x, y = 1 };

            //while (atMap(down) == ' ')
            //{
            //    down = new Point { x = p.x, y = down.y + 1 };
            //}

            return (down, facing);
        };

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
                            (Point newPoint, int newFacing) = up(currentPoint, facing);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                                facing = newFacing;
                            }
                            break;
                        }
                    case 0:
                        {
                            (Point newPoint, int newFacing) = right(currentPoint, facing);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                                facing = newFacing;
                            }
                            break;
                        }
                    case 1:
                        {
                            (Point newPoint, int newFacing) = down(currentPoint, facing);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                                facing = newFacing;
                            }
                            break;
                        }
                    case 2:
                        {
                            (Point newPoint, int newFacing) = left(currentPoint, facing);
                            if (atMap(newPoint) == '#')
                            {
                                stop = true;
                            }
                            else
                            {
                                currentPoint = newPoint;
                                facing = newFacing;
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
