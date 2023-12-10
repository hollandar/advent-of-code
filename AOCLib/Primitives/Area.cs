﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AOCLib.Primitives
{
    public class Area
    {
        List<Bounds> bounds;

        public Area(params Bounds[] bounds)
        {
            this.bounds = bounds.ToList();
        }

        public long MinimumX => bounds.Min(b => b.TopLeft.X);
        public long MinimumY => bounds.Min(b => b.TopLeft.Y);
        public long MaximumX => bounds.Max(b => b.BottomRight.X);
        public long MaximumY => bounds.Max(b => b.BottomRight.Y);

        public bool Contains(Point p)
        {
            return bounds.Any(b => b.Contains(p));
        }

        public Point Right(Point p)
        {
            var right = p.East();
            if (!Contains(right))
            {
                right = new Point(MinimumX, p.Y);
                while (!Contains(right))
                    right.MoveRight(1);
            }

            return right;
        }

        public Point Down(Point p)
        {
            var down = p.South();
            if (!Contains(down))
            {
                down = new Point(p.X, MinimumY);
                while (!Contains(down))
                    down.MoveDown(1);
            }

            return down;
        }

        public Point Left(Point p)
        {
            var left = p.West();
            if (!Contains(left))
            {
                left = new Point(MaximumX, p.Y);
                while (!Contains(left))
                    left.MoveLeft(1);
            }

            return left;
        }

        public Point Up(Point p)
        {
            var up = p.North();
            if (!Contains(up))
            {
                up = new Point(p.X, MaximumY);
                while (!Contains(up))
                    up.MoveUp(1);
            }

            return up;
        }


    }
}
