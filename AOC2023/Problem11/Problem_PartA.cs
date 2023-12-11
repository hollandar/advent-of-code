using AOCLib;
using AOCLib.Primitives;

namespace AdventOfCode2023.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        var grid = new CharacterGrid(datas.Select(r => r.Value));

        Console.WriteLine(grid.Bounds.ToString());
        for (int x = 0; x < grid.Width; x++)
        {
            if (grid.Column(x).All(r => r == '.')) {
                grid.InsertColumn(x, Enumerable.Repeat('.', grid.Height));
                x++;
            }
        }

        for (int y = 0; y < grid.Height; y++)
        {
            if (grid.Row(y).All(r => r == '.'))
            {
                grid.InsertRow(y, Enumerable.Repeat('.', grid.Width));
                y++;
            }
        }
        Console.WriteLine(grid.Bounds.ToString());


        List<Point> galaxies = new();
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                var point = new Point(x, y);
                if (grid[point] == '#')
                {
                    galaxies.Add(point);
                }
            }
        }

        foreach (var galaxy in galaxies)
        {
            foreach (var otherGalaxy in galaxies.SkipWhile(g => g != galaxy))
            {
                answer += (int)galaxy.ManhattanDisanceTo(otherGalaxy);
            }
        }

        return answer.ToString();
    }

}
