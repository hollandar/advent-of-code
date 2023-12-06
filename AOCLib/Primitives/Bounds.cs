using System.Diagnostics;
using System.Numerics;

namespace AOCLib.Primitives
{
    public enum Orientation { _0, _90, _180, _270 }

    public struct Bounds
    {
        public Point TopLeft { get; }
        public Point BottomRight { get; }
        public Orientation Orientation { get; }
        
        public Bounds(Point topLeft, Point bottomRight, Orientation orientation = Orientation._0)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
            this.Orientation = orientation;
        }
        public long Top => TopLeft.Y;
        public long Bottom => BottomRight.Y;
        public long Left => TopLeft.X;
        public long Right => BottomRight.X;

        public Bounds LocalBounds => new Bounds(new Point(0, 0), new Point(Right - Left, Bottom - Top));

        public bool Contains(Point p)
        {
            return p.WithinBounds(TopLeft, BottomRight);
        }

        public Point ToLocal(Point p)
        {
            Debug.Assert(Contains(p), $"The point {p} is not within the bounds {this}");
            var unadjustedLocal = new Point(p.X - TopLeft.X, p.Y - TopLeft.Y);
            return Orientation switch
            {
                Orientation._0 => unadjustedLocal,
                Orientation._270 => RotateLocal90(unadjustedLocal),
                Orientation._180 => RotateLocal90(RotateLocal90(unadjustedLocal)),
                Orientation._90 => RotateLocal90(RotateLocal90(RotateLocal90(unadjustedLocal))),
            };
        }

        public Point FromLocal(Point p)
        {
            var unadjustedLocal = Orientation switch
            {
                Orientation._0 => p,
                Orientation._90 => RotateLocal90(p),
                Orientation._180 => RotateLocal90(RotateLocal90(p)),
                Orientation._270 => RotateLocal90(RotateLocal90(RotateLocal90(p))),
            };
            var result = new Point(unadjustedLocal.X + TopLeft.X, unadjustedLocal.Y + TopLeft.Y);
            Debug.Assert(Contains(result), $"The point {result} is not within the bounds {this}");
            return result;
        }

        public static bool operator ==(Bounds a, Bounds b) => a.TopLeft == b.TopLeft && a.BottomRight == b.BottomRight;
        public static bool operator !=(Bounds a, Bounds b) => !(a == b);

        private Point RotateLocal90(Point p)
        {
            return new Point(this.LocalBounds.Bottom - p.Y, p.X);

        }
    }
}
