using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOCLib.Primitives
{
    public class Point
    {
        int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Point()
        {

        }

        public Point(Point p)
        {
            this.x = p.x;
            this.y = p.y;
        }

        public int X => x;
        public int Y => y;

        public double AngularDistanceTo(Point p)
        {
            var distance = Math.Abs(Math.Sqrt(Math.Pow((p.x - x), 2) + Math.Pow((p.y - y), 2)));
            return distance;
        }

        public long ManhattanDisanceTo(Point point)
        {
            return Math.Abs(x - point.x) + Math.Abs(y - point.y);
        }

        public bool Equals(Point point)
        {
            return x == point.x && y == point.y;
        }

        public override bool Equals(object? point)
        {
            if (!(point is Point)) return false;
            return Equals((Point)point);
        }

        public override int GetHashCode()
        {
            return x + (y + int.MaxValue / 2);
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public bool AdjacentTo(Point point)
        {
            int xadj = Math.Abs(point.x - x);
            int yadj = Math.Abs(point.y - y);

            return xadj <= 1 && yadj <= 1;
        }

        public void MoveUp(int distance)
        {
            y -= distance;
        }
        public void MoveDown(int distance)
        {
            y += distance;
        }
        public void MoveLeft(int distance)
        {
            x -= distance;
        }
        public void MoveRight(int distance)
        {
            x += distance;
        }

        public static bool operator ==(Point a, Point b) => a.Equals(b);
        public static bool operator !=(Point a, Point b) => !a.Equals(b);

    }
}
