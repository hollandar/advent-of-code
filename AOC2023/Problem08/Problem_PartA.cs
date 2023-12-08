using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem08;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        var instructions = datas.First().Value;
        var lrLregex = new Regex("^(...) = [(](...), (...)[)]$");
        var moves = new Dictionary<string, (string, string)>();
        foreach (var data in datas.Skip(2))
        {
            var match = lrLregex.Match(data.Value);
            if (match.Success)
            {
                moves[match.Groups[1].Value] = (match.Groups[2].Value, match.Groups[3].Value);
            }
        }

        var start = "AAA";
        var end = "ZZZ";
        var location = start;
        var i = 0;
        do
        {
            var move = instructions[i];
            i++;
            if (i == instructions.Length) i = 0;

            var (left, right) = moves[location];

            location = move switch
            {
                'L' => left,
                'R' => right,
                _ => throw new Exception("Unknown move")
            };
            answer++;

        } while (location != end);


        return answer.ToString();
    }

}
