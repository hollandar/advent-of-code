using AOCLib;

namespace AdventOfCode2022.Problem01;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;

        int calories = 0;

        foreach (var data in datas)
        {
            if (string.IsNullOrWhiteSpace(data.Value))
            {
                if (calories > answer)
                {
                    answer = calories;
                }
                calories = 0;
                continue;

            }
            else
            {
                calories += int.Parse(data.Value);
            }
        }

        if (calories > answer)
        {
            answer = calories;
        }

        return answer.ToString();
    }

}
