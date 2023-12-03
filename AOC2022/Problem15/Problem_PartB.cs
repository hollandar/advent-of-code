using AOCLib;
using AOCLib.Primitives;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem15;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var sensors = Load(datas);

        const int area = 4000000;

        // Instead of brute forcing an area 4000000x4000000, we can use the fact that the manhattan distance around the sensor is a straight
        // line on each edge.
        foreach (var sensor in sensors)
        {
            // Search 1 point outside each sensors range for a point that is not covered by any other sensor.
            var distance = sensor.sensor.ManhattanDisanceTo(sensor.beacon) + 1;


            // This is essentially the same as part a
            Func<Line, Point?> search = (line) =>
            {
                for (long x = 0; x <= area; x++)
                {
                    bool covered = false;
                    
                    // Given x, we work out the y value on the line
                    long y = line.Y_GivenX(x);

                    // For the sample, this constraint must be imposed to get the correct result.
                    if (InSample) 
                        if (y > 20 || x > 20) return null;

                    // Not we make sure no other sensors cover this point
                    // If one does, we bail
                    var point = new Point(x, y);
                    foreach (var sensor in sensors)
                    {
                        if (sensor.InRange(new Point(x, y)))
                        {
                            covered = true;
                            break;
                        };
                    }
                    if (!covered)
                        return point;

                }

                return null;

            };

            Line line;
            Point? point;

            // Consider the bottom right boundary of the sensor
            line = Line.FromPoints(
                new Point(sensor.sensor.X, sensor.sensor.Y - distance),
                new Point(sensor.sensor.X + distance, sensor.sensor.Y)
            );
            point = search(line);

            if (point is not null)
            {
                if (point.WithinBounds(area, area))
                {
                    DebugLn(point.ToString());
                    return (point.X * area + point.Y).ToString();
                }
            }

            // Consider the bottom left boundary of the sensor
            line = Line.FromPoints(
                new Point(sensor.sensor.X, sensor.sensor.Y - distance),
                new Point(sensor.sensor.X - distance, sensor.sensor.Y)
            );
            point = search(line);

            if (point is not null)
            {
                if (point.WithinBounds(area, area))
                {
                    DebugLn(point.ToString());
                    return (point.X * area + point.Y).ToString();
                }
            }

            // Consider the top right boundary of the sensor
            line = Line.FromPoints(
                new Point(sensor.sensor.X, sensor.sensor.Y + distance),
                new Point(sensor.sensor.X + distance, sensor.sensor.Y)
            );
            point = search(line);

            if (point is not null)
            {
                if (point.WithinBounds(area, area))
                {
                    DebugLn(point.ToString());
                    return (point.X * area + point.Y).ToString();
                }
            }

            // Consider the top left boundary of the sensor
            line = Line.FromPoints(
                new Point(sensor.sensor.X, sensor.sensor.Y + distance),
                new Point(sensor.sensor.X - distance, sensor.sensor.Y)
            );
            point = search(line);

            if (point is not null)
            {
                if (point.WithinBounds(area, area))
                {
                    DebugLn(point.ToString());
                    return (point.X * area + point.Y).ToString();
                }
            }
        }

        // Should never get here
        return "All points covered!";
    }

}
