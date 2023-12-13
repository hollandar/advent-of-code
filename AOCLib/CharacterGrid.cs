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

        public IEnumerable<char> Row(int r)
        {
            for (int x = 0; x < width; x++)
            {
                yield return cells[r, x];
            }
        }

        public IEnumerable<char> Column(int c)
        {
            for (int y = 0; y < height; y++)
            {
                yield return cells[y, c];
            }
        }

        public void InsertColumn(int c, IEnumerable<char> values)
        {
            Debug.Assert(values.Count() == height);
            Debug.Assert(c >= 0 && c <= width);

            var newCells = new char[height, width + 1];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0 ; x < c; x++)
                {
                    newCells[y, x] = cells[y, x];
                }
                newCells[y, c] = values.ElementAt(y);
                for (int x = c; x < width; x++)
                {
                    newCells[y, x + 1] = cells[y, x];
                }
            }

            cells = newCells;
            width++;
            bounds = new Bounds(new Point(0, 0), new Point(width - 1, height - 1));
        }

        public void InsertRow(int r, IEnumerable<char> values)
        {
            Debug.Assert(values.Count() == width);
            Debug.Assert(r >= 0 && r <= height);

            var newCells = new char[height + 1, width];
            for (int y = 0; y < r; y++)
            {
                for (int x = 0 ; x < width; x++)
                {
                    newCells[y, x] = cells[y, x];
                }
            }
            for (int x = 0; x < width; x++)
            {
                newCells[r, x] = values.ElementAt(x);
            }
            for (int y = r; y < height; y++)
            {
                for (int x = 0 ; x < width; x++)
                {
                    newCells[y + 1, x] = cells[y, x];
                }
            }

            cells = newCells;
            height++;
            bounds = new Bounds(new Point(0, 0), new Point(width - 1, height - 1));
        } 
    }
}
