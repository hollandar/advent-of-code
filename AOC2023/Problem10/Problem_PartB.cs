using AOCLib;
using AOCLib.Primitives;

namespace AdventOfCode2023.Problem10;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var characterGrid = new CharacterGrid(datas.Select(d => d.Value));
        Point start = characterGrid.FindSingle('S');
        // Find all the points on the path
        var pathPonts = TraversePath(start, characterGrid);

        // Find the first top left corner of the path on the map, hopefully a good clockwise traversal starting point
        var found = false;
        for (int y = 0; y < characterGrid.Bounds.Height; y++)
        {
            for (int x = 0; x < characterGrid.Bounds.Width; x++)
            {
                var p = new Point(x, y);
                if (characterGrid[p] == 'F' && pathPonts.Contains(p))
                {
                    start = p;
                    found = true;
                    break;
                }
            }
            if (found) { break; }
        }


        var visitedPoint = new HashSet<Point>();
        var pointsInside = new HashSet<Point>();


        Point currentPoint = start;
        Direction direction = Direction.North;
        int stationaryCount = 0;
        // Traverse in a clockwise direction, taking points to the right when facing north, south when facing east, west when facing south, and north when facing west.
        while (stationaryCount < 10)
        {
            if (direction == Direction.North && !visitedPoint.Contains(currentPoint.North()))
            {
                if (!pathPonts.Contains(currentPoint.East()))
                {
                    pointsInside.Add(currentPoint.East());
                }
                if (characterGrid.Bounds.Contains(currentPoint.North()) && Connected(characterGrid[currentPoint], characterGrid[currentPoint.North()], Direction.North))
                {
                    visitedPoint.Add(currentPoint);
                    currentPoint = currentPoint.North();
                    stationaryCount = 0;
                    continue;
                }
            }

            if (direction == Direction.East && !visitedPoint.Contains(currentPoint.East()))
            {
                if (!pathPonts.Contains(currentPoint.South()))
                {
                    pointsInside.Add(currentPoint.South());
                }
                if (characterGrid.Bounds.Contains(currentPoint.East()) && Connected(characterGrid[currentPoint], characterGrid[currentPoint.East()], Direction.East))
                {
                    visitedPoint.Add(currentPoint);
                    currentPoint = currentPoint.East();
                    stationaryCount = 0;
                    continue;
                }
            }

            if (direction == Direction.South && !visitedPoint.Contains(currentPoint.South()))
            {
                if (!pathPonts.Contains(currentPoint.West()))
                {
                    pointsInside.Add(currentPoint.West());
                }
                if (characterGrid.Bounds.Contains(currentPoint.South()) && Connected(characterGrid[currentPoint], characterGrid[currentPoint.South()], Direction.South))
                {
                    visitedPoint.Add(currentPoint);
                    currentPoint = currentPoint.South();
                    stationaryCount = 0;
                    continue;
                }
            }

            if (direction == Direction.West && !visitedPoint.Contains(currentPoint.West()))
            {
                if (!pathPonts.Contains(currentPoint.North()))
                {
                    pointsInside.Add(currentPoint.North());
                }
                if (characterGrid.Bounds.Contains(currentPoint.West()) && Connected(characterGrid[currentPoint], characterGrid[currentPoint.West()], Direction.West))
                {
                    visitedPoint.Add(currentPoint);
                    currentPoint = currentPoint.West();
                    stationaryCount = 0;
                    continue;
                }
            }

            direction = direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException()
            };

            stationaryCount++;
        }


        // Flood fill out from the found inside points to cover others that arent on the path
        while (true)
        {
            bool moved = false;
            foreach (var point in pointsInside.ToList())
            {
                var adjacencies = point.AdjacentPoints(characterGrid.Bounds);
                foreach (var adjacency in adjacencies)
                {
                    if (!pathPonts.Contains(adjacency) && !pointsInside.Contains(adjacency))
                    {
                        pointsInside.Add(adjacency);
                        moved = true;
                    }
                }
            }

            if (!moved) break;
        }

        if (InDebug)
        {
            characterGrid.Dump((ConsoleColor.Red, pathPonts), (ConsoleColor.Green, pointsInside));
        }
        return pointsInside.Count.ToString();
    }

    // Perform a bredth first search to the farthest point from the start.
    // Assume from the instructions that it is a loop.
    HashSet<Point> TraversePath(Point position, CharacterGrid grid, int currentDistance = 0)
    {
        Queue<(int distance, Point point, Point nextPoint, Direction direction)> points = new();
        HashSet<Point> visitedPoints = new() { position };
        points.Enqueue((0, position, position.North(), Direction.North));
        points.Enqueue((0, position, position.South(), Direction.South));
        points.Enqueue((0, position, position.West(), Direction.West));
        points.Enqueue((0, position, position.East(), Direction.East));


        while (points.Count > 0)
        {
            var cell = points.Dequeue();
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

        return visitedPoints;
    }

}
