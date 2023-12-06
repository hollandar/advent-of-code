using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem06;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        var row0 = datas.ElementAt(0).Value;
        var row1 = datas.ElementAt(1).Value;

        var r = new Regex("\\d+");
        var times = ulong.Parse(String.Join("", r.Matches(row0).Select(m => m.Value).ToArray()));
        var distances = ulong.Parse(String.Join("", r.Matches(row1).Select(m => m.Value)).ToArray());

        for (ulong holdTime = 0; holdTime < times; holdTime++)
        {
            var raceTime = times - holdTime;

            var distance = holdTime * raceTime;

            bool wins = distance > distances;

            DebugLn($"{holdTime} => speed {holdTime}mm/mi race time {raceTime} => distance {distance} {(wins ? "WIN" : "loss")}");

            if (wins) answer++;

        }

        return answer.ToString();
    }

}
