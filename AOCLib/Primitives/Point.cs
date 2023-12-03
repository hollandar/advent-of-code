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
        long x, y;
        public Point(long x, long y)
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

        public long X => x;
        public long Y => y;

        public double AngularDistanceTo(Point p)
        {
            var distance = Math.Abs(Math.Sqrt(Math.Pow((p.x - x), 2) + Math.Pow((p.y - y), 2)));
            return distance;
        }

        public long ManhattanDisanceTo(Point point)
        {
            return Math.Abs(x - point.x) + Math.Abs(y - point.y);
        }

        public bool Equals(Point? point)
        {
            if (point is null) return false;
            return x == point.x && y == point.y;
        }

        public override bool Equals(object? point)
        {
            if (!(point is Point)) return false;
            return Equals((Point)point);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public bool AdjacentTo(Point point)
        {
            long xadj = Math.Abs(point.x - x);
            long yadj = Math.Abs(point.y - y);

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

        public bool WithinBounds(Point topLeft, Point bottomRight)
        {
            var inX = X >= topLeft.X && X <= bottomRight.X;
            var inY = Y >= topLeft.Y && Y <= bottomRight.Y;
            return inX && inY;
        }

        public bool WithinBounds(Point bottomRight) => WithinBounds(Zero, bottomRight);
        public bool WithinBounds(long x2, long y2) => WithinBounds(Zero, new Point(x2, y2));
        public bool WithinBounds(long x1, long y1, long x2, long y2) => WithinBounds(new Point(x1, y1), new Point(x2, y2));

        public static Point Zero = new Point(0, 0);
        public static bool operator ==(Point? a, Point? b) => a?.Equals(b) ?? false;
        public static bool operator !=(Point? a, Point? b) => !(a == b);

    }
}
