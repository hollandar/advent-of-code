﻿namespace AOCLib.Primitives
{
    public struct Bounds
    {
        public Point TopLeft { get; }
        public Point BottomRight { get; }
        
        public Bounds(Point topLeft, Point bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        public bool Contains(Point p)
        {
            return p.WithinBounds(TopLeft, BottomRight);
        }
    }
}
