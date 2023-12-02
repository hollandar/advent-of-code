using AOCLib;
using AOCLib.AStar;
using AOCLib.Primitives;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem12;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem12", RowRegex());
        RunPartB("Problem12", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    public struct Terrain
    {
        public int Width;
        public int Height;
        public List<char[]> Grid;
        public List<Node> Nodes;
        public List<Node> Lowest;
        public Node Start;
        public Node End;
    }

    protected Terrain Load(IEnumerable<InputRow> datas)
    {
        var grid = new Terrain { Grid = new List<char[]>() };
        int w = 0;
        foreach (var line in datas)
        {
            if (grid.Width != 0)
            {
                System.Diagnostics.Debug.Assert(grid.Width == line.Value.Length);
            }
            else
            {
                grid.Width = line.Value.Length;
            }
            grid.Grid.Add(line.Value.ToCharArray());
        }
        grid.Height = grid.Grid.Count;

        Node? start = null;
        Node? end = null;
        List<Node> nodes = new List<Node>();
        List<Node> lowest = new List<Node>();
        Dictionary<Point, Node> nodesByPoint = new();
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {

                var findNode = (int x, int y) =>
                {
                    if (!nodesByPoint.TryGetValue(new Point(x, y), out var node))
                    {
                        node = new Node(new Point(x, y));
                        nodes.Add(node);
                        nodesByPoint[node.Point] = node;
                    }

                    return node;
                };

                if (grid.Grid[y][x] == 'S')
                {
                    start = findNode(x, y);
                    grid.Grid[y][x] = 'a';
                }
                if (grid.Grid[y][x] == 'E')
                {
                    end = findNode(x, y);
                    grid.Grid[y][x] = 'z';
                }


                var fromNode = findNode(x, y);
                if (grid.Grid[y][x] == 'a')
                {
                    lowest.Add(fromNode);
                }

                if (x > 0)
                {
                    int height = grid.Grid[y][x - 1] - grid.Grid[y][x];
                    if (height <= 1)
                    {
                        var toNode = findNode(x - 1, y);
                        fromNode.ConnectEdge(toNode, 1);
                    }
                }

                if (y > 0)
                {
                    int height = grid.Grid[y - 1][x] - grid.Grid[y][x];
                    if (height <= 1)
                    {
                        var toNode = findNode(x, y - 1);
                        fromNode.ConnectEdge(toNode, 1);
                    }
                }

                if (x < grid.Width - 1)
                {
                    int height = grid.Grid[y][x + 1] - grid.Grid[y][x];
                    if (height <= 1)
                    {
                        var toNode = findNode(x + 1, y);
                        fromNode.ConnectEdge(toNode, 1);
                    }
                }

                if (y < grid.Height - 1)
                {
                    int height = grid.Grid[y + 1][x] - grid.Grid[y][x];
                    if (height <= 1)
                    {
                        var toNode = findNode(x, y + 1);
                        fromNode.ConnectEdge(toNode, 1);
                    }
                }
            }
        }

        grid.Start = start;
        grid.End = end;
        grid.Nodes = nodes;
        grid.Lowest = lowest;

        return grid;
    }
}

