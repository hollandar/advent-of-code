using AOCLib;
using AOCLib.Primitives;

namespace AdventOfCode2023.Problem10;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var characterGrid = new CharacterGrid(datas.Select(d => d.Value));
        var start = characterGrid.FindSingle('S');

        var distance = TraverseLoopToExtents(start, characterGrid);


        return distance.ToString();
    }

    // Perform a bredth first search to the farthest point from the start.
    // Assume from the instructions that it is a loop, working out along both directions from the start.
    int TraverseLoopToExtents(Point position, CharacterGrid grid, int currentDistance = 0)
    {
        Queue<(int distance, Point point, Point nextPoint, Direction direction)> points = new();
        HashSet<Point> visitedPoints = new() { position };
        points.Enqueue((0, position, position.North(), Direction.North));
        points.Enqueue((0, position, position.South(), Direction.South));
        points.Enqueue((0, position, position.West(), Direction.West));
        points.Enqueue((0, position, position.East(), Direction.East));


        int finalDistance = 0;
        while (points.Count > 0)
        {
            var cell = points.Dequeue();
            finalDistance = Math.Max(cell.distance, finalDistance);
            if (visitedPoints.Contains(cell.nextPoint))
            {
                continue;
            }


            if (grid.Bounds.Contains(cell.nextPoint) && Connected(grid[cell.point], grid[cell.nextPoint], cell.direction))
            {
                visitedPoints.Add(cell.nextPoint);
                points.Enqueue((cell.distance + 1, cell.nextPoint, cell.nextPoint.North(), Direction.North));
                points.Enqueue((cell.distance + 1, cell.nextPoint, cell.nextPoint.South(), Direction.South));
                points.Enqueue((cell.distance + 1, cell.nextPoint, cell.nextPoint.West(), Direction.West));
                points.Enqueue((cell.distance + 1, cell.nextPoint, cell.nextPoint.East(), Direction.East));
            }
        }

        return finalDistance;
    }

   

}
