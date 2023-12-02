using AOCLib;
using AOCLib.Primitives;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem09;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem09", RowRegex());
        RunPartB("Problem09", RowRegex());
    }

    [GeneratedRegex("^(?<Direction>.) (?<Distance>\\d+)$")]
    public static partial Regex RowRegex();

    class Knot
    {
        protected Point point = new Point();
        public Knot()
        {

        }

        public Point Point => point;
    }

    class Head : Knot
    {
        public void Move(Move move)
        {
            switch (move.Direction)
            {
                case DirectionEnum.Up:
                    point.MoveUp(move.Distance);
                    break;
                case DirectionEnum.Down:
                    point.MoveDown(move.Distance);
                    break;
                case DirectionEnum.Left:
                    point.MoveLeft(move.Distance);
                    break;
                case DirectionEnum.Right:
                    point.MoveRight(move.Distance);
                    break;
            }
        }

        public override string ToString()
        {
            return $"Head {point}";
        }
    }

    class Tail : Knot
    {
        HashSet<Point> visitedPoints = new HashSet<Point>();

        public HashSet<Point> VisitedPoints => visitedPoints;

        public void MoveRelativeTo(Knot head)
        {
            visitedPoints.Add(new Point(Point));

            if (head.Point.AdjacentTo(Point)) return;

            if (head.Point.X == Point.X)
            {
                if (head.Point.Y < point.Y)
                {
                    Point.MoveUp(1);
                }
                else if (head.Point.Y > point.Y)
                {
                    Point.MoveDown(1);
                }
            }

            else if (head.Point.Y == Point.Y)
            {
                if (head.Point.X < point.X)
                {
                    Point.MoveLeft(1);
                }
                else if (head.Point.X > point.X)
                {
                    Point.MoveRight(1);
                    return;
                }
            }
            else if (head.Point.X < Point.X)
            {
                if (head.Point.Y < point.Y)
                {
                    Point.MoveLeft(1);
                    Point.MoveUp(1);
                }
                else if (head.Point.Y > point.Y)
                {
                    Point.MoveLeft(1);
                    Point.MoveDown(1);
                }
            }

            else if (head.Point.Y < Point.Y)
            {
                if (head.Point.X < point.X)
                {
                    Point.MoveUp(1);
                    Point.MoveLeft(1);
                }
                else if (head.Point.X > point.X)
                {
                    Point.MoveUp(1);
                    Point.MoveRight(1);
                }
            }

            else if (head.Point.X < Point.X)
            {
                if (head.Point.Y < point.Y)
                {
                    Point.MoveLeft(1);
                    Point.MoveUp(1);
                }
                else if (head.Point.Y > point.Y)
                {
                    Point.MoveLeft(1);
                    Point.MoveDown(1);
                }
            }

            else if (head.Point.Y > Point.Y)
            {
                if (head.Point.X < point.X)
                {
                    Point.MoveDown(1);
                    Point.MoveLeft(1);
                }
                else if (head.Point.X > point.X)
                {
                    Point.MoveDown(1);
                    Point.MoveRight(1);
                }
            }

            else if (head.Point.X > Point.X)
            {
                if (head.Point.Y < point.Y)
                {
                    Point.MoveRight(1);
                    Point.MoveUp(1);
                }
                else if (head.Point.Y > point.Y)
                {
                    Point.MoveRight(1);
                    Point.MoveDown(1);
                }
            }

            visitedPoints.Add(new Point(Point));
        }

        public override string ToString()
        {
            return $"Tail {point}";
        }
    }

    public enum DirectionEnum { Up, Down, Left, Right }
    public class Move
    {
        DirectionEnum direction;
        int distance;

        public Move(DirectionEnum direction, int distance)
        {
            this.direction = direction;
            this.distance = distance;
        }

        public DirectionEnum Direction => direction;
        public int Distance => distance;
    }

    protected List<Move> Load(IEnumerable<InputRow> datas)
    {
        List<Move> moves = new();

        foreach (var line in datas)
        {
            var directionString = line.Direction;
            var distanceString = line.Distance;
            var direction = directionString switch
            {
                "U" => DirectionEnum.Up,
                "D" => DirectionEnum.Down,
                "L" => DirectionEnum.Left,
                "R" => DirectionEnum.Right,
                _ => throw new Exception()
            };
            var move = new Move(
                direction,
                int.Parse(distanceString)
            );

            moves.Add(move);
        }

        return moves;
    }

}

