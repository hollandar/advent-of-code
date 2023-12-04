using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem04;

public partial class Problem : ProblemPart<InputRow>
{
    [GeneratedRegex("(?<Value>\\d+|\\|)")]
    protected partial Regex NumberRegex();

    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        foreach (var data in datas)
        {
            var matches = NumberRegex().Matches(data.Value);
            var matchValues = matches.Select(r => r.Value);
            var cardNumber = int.Parse(matchValues.First());
            var winningNumbers = matchValues.Skip(1).TakeWhile(r => r != "|").Select(r => int.Parse(r)).ToHashSet();
            var myNumbers = matchValues.SkipWhile(r => r != "|").Skip(1).Select(r => int.Parse(r)).ToHashSet();

            var winningCount = winningNumbers.Intersect(myNumbers).Count();
            var value = winningCount > 0 ? 1: 0;
            for (int i = 1; i < winningCount; i++)
            {
                value = value * 2;
            }

            answer += value;
        }

        return answer.ToString();
    }

}
