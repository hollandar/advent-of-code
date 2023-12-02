using AOCLib;

namespace AdventOfCode2022.Problem01;

public partial class Problem : ProblemPart<InputRow>
{
    protected override long PartB(IEnumerable<InputRow> datas)
    {
        long answer = 0;

        var elves = new List<int>();
        int calories = 0;

        foreach (var data in datas)
        {
            if (string.IsNullOrWhiteSpace(data.Value))
            {
                elves.Add(calories);
                calories = 0;
                continue;

            }
            else
            {
                calories += int.Parse(data.Value);
            }
        }

        elves.Add(calories);

        answer = elves.OrderByDescending(x => x).Take(3).Sum();

        return answer;
    }

}
