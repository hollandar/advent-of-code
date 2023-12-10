using AOCLib.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCLib
{
    public class CharacterGrid
    {
        private int width;
        private int height;
        private Bounds bounds;
        private char[,] cells;

        public CharacterGrid(IEnumerable<string> rows)
        {
            Debug.Assert(rows.Count() > 0);
            this.width = rows.First().Length;

            foreach (var row in rows) { 
                Debug.Assert(row.Length == width);
            }

            this.height = rows.Count();
            this.bounds = new Bounds(new Point(0, 0), new Point(width - 1, height - 1));

            cells = new char[height,width];

            for (int y = 0 ; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[y, x] = rows.ElementAt(y)[x];
                }
            }
        }

        public int Width => width;
        public int Height => height;
        public Bounds Bounds => bounds;

        public char this[int x, int y]
        {
            get
            {
                Debug.Assert(new Point(x, y).WithinBounds(bounds));
                return cells[y, x];
            }
            set
            {
                cells[y, x] = value;
            }
        }

        public char this[Point p]
        {
            get
            {
                Debug.Assert(p.WithinBounds(bounds));
                return cells[p.Y, p.X];
            }
            set
            {
                cells[p.Y, p.X] = value;
            }
        }

        public string SubstringHorizontal(Point start, long length)
        {
            Debug.Assert(start.WithinBounds(bounds));
            Debug.Assert(start.X + length <= width);

            var p = start;
            var result = new char[length];
            for (long i = 0; i < length; i++)
            {
                result[i] = this[p];
                p = p.East();
            }

            return new string(result);
        }

        public Point FindSingle(char c)
        {
            Point? result = null;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0 ; x < width; x++)
                {
                    if (cells[y, x] == c)
                    {
                        if (result is not null) throw new Exception("Multiple instances of " + c);
                        result = new Point(x, y);
                    }
                }
            }

            return result!;
        }

        public void Dump(params (ConsoleColor color, HashSet<Point> points)[] highlights)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0 ; x < width; x++)
                {
                    ConsoleColor displayColor = ConsoleColor.White;
                    foreach (var (color, points) in highlights)
                    {
                        if (points.Contains(new Point(x, y)))
                        {
                            displayColor = color;
                        }
                    }
                    Console.ForegroundColor = displayColor;
                    Console.Write(cells[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("--");
        }
    }
}
