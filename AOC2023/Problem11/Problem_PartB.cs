using AOCLib;
using AOCLib.Primitives;

namespace AdventOfCode2023.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        long answer = 0;
        var grid = new CharacterGrid(datas.Select(r => r.Value));

        // Instead of exanding the grid, this time we just expand that galaxy positions
        List<int> expansionsX = new();
        List<int> expansionsY = new();
        for (int x = 0; x < grid.Width; x++)
        {
            if (grid.Column(x).All(r => r == '.'))
            {
                expansionsX.Add(x);
            }
        }

        for (int y = 0; y < grid.Height; y++)
        {
            if (grid.Row(y).All(r => r == '.'))
            {
                expansionsY.Add(y);
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

        int multiplier = (InSample ? 100 : 1000000) - 1;
        foreach (var galaxy in galaxies)
        {
            var startGalaxy = new Point(
                galaxy.X + expansionsX.Where(x => x < galaxy.X).Count() * multiplier,
                galaxy.Y + expansionsY.Where(y => y < galaxy.Y).Count() * multiplier
                );
            foreach (var otherGalaxy in galaxies.SkipWhile(g => g != galaxy))
            {
                var endGalaxy = new Point(
                    otherGalaxy.X + expansionsX.Where(x => x < otherGalaxy.X).Count() * multiplier,
                    otherGalaxy.Y + expansionsY.Where(y => y < otherGalaxy.Y).Count() * multiplier
                    );
                answer += startGalaxy.ManhattanDisanceTo(endGalaxy);
            }
        }

        return answer.ToString();
    }

}
