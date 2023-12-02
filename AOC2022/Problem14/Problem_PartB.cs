using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem14;

public partial class Problem : ProblemPart<InputRow>
{
    [GeneratedRegex(@"(\d+),(\d+)")]
    protected partial Regex PairRegex();

    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var pairRegex = PairRegex();

        List<List<Cell>> cells = new List<List<Cell>>();
        for (int y = 0; y < 1000; y++)
        {
            cells.Add(new List<Cell>());
            for (int x = 0; x < 1000; x++)
            {
                cells[y].Add(new Cell());
            }
        }

        foreach (var data in datas)
        {
            var line = data.Value;

            var matches = pairRegex.Matches(line);

            for (int i = 0; i < matches.Count - 1; i++)
            {
                var p1 = matches[i];
                var x1 = int.Parse(p1.Groups[1].Value);
                var y1 = int.Parse(p1.Groups[2].Value);

                var p2 = matches[i + 1];
                var x2 = int.Parse(p2.Groups[1].Value);
                var y2 = int.Parse(p2.Groups[2].Value);

                DebugLn($"{x1}, {y1} -> {x2}, {y2}");

                if (y1 == y2)
                {
                    var xFill = new int[] { x1, x2 }.Order().ToArray();
                    for (int x = xFill[0]; x <= xFill[1]; x++)
                    {
                        cells[y1][x].rock = true;
                    }
                }

                if (x1 == x2)
                {
                    var yFill = new int[] { y1, y2 }.Order().ToArray();
                    for (int y = yFill[0]; y <= yFill[1]; y++)
                    {
                        cells[y][x1].rock = true;
                    }
                }
            }
        }


        // Build a floor of rock at the appropriate location
        int floorLevel = 0;
        for (int y = 0; y < cells.Count; y++)
        {
            for (int x = 0; x < cells[y].Count; x++)
            {
                if (cells[y][x].rock && y > floorLevel)
                {
                    floorLevel = y;
                }
            }
        }

        floorLevel += 2;

        for (int x = 0; x < cells[floorLevel].Count; x++)
        {
            cells[floorLevel][x].rock = true;
        }

        int sandX = 500, sandY = 0;
        var itsOver = false;
        do
        {
            sandX = 500;
            sandY = 0;
            while (!itsOver)
            {
                if (!cells[sandY + 1][sandX].rock && !cells[sandY + 1][sandX].sand)
                {
                    sandY++;
                }
                else if (!cells[sandY + 1][sandX - 1].rock && !cells[sandY + 1][sandX - 1].sand)
                {
                    sandX--;
                    sandY++;
                }
                else if (!cells[sandY + 1][sandX + 1].rock && !cells[sandY + 1][sandX + 1].sand)
                {
                    sandX++;
                    sandY++;
                }
                else
                {
                    if (sandY == 0 && sandX == 500)
                        itsOver = true;
                    cells[sandY][sandX].sand = true;
                    break;
                }
            }


        } while (!itsOver);  // Sand fell off the bottom, done

        var restingSand = 0;
        for (int y = 0; y < cells.Count; y++)
        {
            for (int x = 0; x < cells[y].Count; x++)
            {
                if (cells[y][x].sand) restingSand++;
            }
        }


        return restingSand.ToString();
    }

}
