using AOCLib;
using AOCLib.Primitives;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem15;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var sensors = Load(datas);

        long answer = 0;
        long y = InSample ? 10 : 2000000;

        // Need to inflate the play area to ensure we get the zones for all sensors
        var minX = Math.Min(sensors.Select(r => r.sensor.X - r.DistanceToBeacon()).Min(), sensors.Select(r => r.beacon.X - r.DistanceToBeacon()).Min());
        var maxX = Math.Max(sensors.Select(r => r.sensor.X + r.DistanceToBeacon()).Max(), sensors.Select(r => r.beacon.X + r.DistanceToBeacon()).Max());

        for (long x = minX; x <= maxX; x++)
        {
            bool inRange = false;
            var checkPoint = new Point(x, y);
            foreach (var sensor in sensors)
            {

                if (sensor.InRange(checkPoint) && sensor.beacon != checkPoint)
                {
                    DebugLn($"Sensor {sensor.sensor} in range of {checkPoint}");
                    inRange = true;
                    break;
                }
            }

            if (!inRange)
            {
                DebugLn($"NO Sensor in range of {x}, {y}");
            }
            else
            {
                answer++;
            }
        }

        return answer.ToString();
    }
}
