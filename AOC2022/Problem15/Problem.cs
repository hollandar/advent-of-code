using AOCLib;
using AOCLib.Primitives;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem15;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem15", RowRegex());
        RunPartB("Problem15", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    class Sensor
    {
        public Point sensor;
        public Point beacon;

        public bool InRange(Point point)
        {
            var beaconDistance = sensor.ManhattanDisanceTo(beacon);
            var pointDistance = sensor.ManhattanDisanceTo(point);

            return pointDistance <= beaconDistance;
        }

        public long DistanceToBeacon()
        {
            return sensor.ManhattanDisanceTo(beacon);
        }
    }

    List<Sensor> Load(IEnumerable<InputRow> datas)
    {
        var coordinateRegex = new Regex(@"x=(-?\d+), y=(-?\d+)");
        List<Sensor> sensors = new();
        foreach (var data in datas)
        {
            var line = data.Value;

            var matches = coordinateRegex.Matches(line);

            Point? sensor = null;
            Point? beacon = null;
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Success)
                {
                    int x = int.Parse(matches[i].Groups[1].Value);
                    int y = int.Parse(matches[i].Groups[2].Value);

                    if (i == 0)
                    {
                        sensor = new Point(x, y);
                    }
                    else
                    {
                        beacon = new Point(x, y);
                    }
                }
                else throw new Exception();
            }

            if (sensor is not null && beacon is not null)
            {
                DebugLn($"{sensor} {beacon}");
                sensors.Add(new Sensor { sensor = sensor, beacon = beacon });
            }

        }
        return sensors;
    }
}

