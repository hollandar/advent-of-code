using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem06;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 1;
        var row0 = datas.ElementAt(0).Value;
        var row1 = datas.ElementAt(1).Value;

        var r = new Regex("\\d+");
        var times = r.Matches(row0).Select(m => int.Parse(m.Value)).ToArray();
        var distances = r.Matches(row1).Select(m => int.Parse(m.Value)).ToArray();

        for (int race = 0; race < times.Length; race++)
        {
            int winCount = 0;
            for (int holdTime = 0; holdTime < times[race]; holdTime++)
            {
                var raceTime = times[race] - holdTime;

                var distance = holdTime * raceTime;

                bool wins = distance > distances[race];

                DebugLn($"Race {race}: {holdTime} => speed {holdTime}mm/mi race time {raceTime} => distance {distance} {(wins ? "WIN" : "loss")}");

                if (wins) winCount++;

            }

            answer *= winCount;

        }
        return answer.ToString();
    }

}
