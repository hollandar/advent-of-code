using AOCLib;

namespace AdventOfCode2022.Problem04;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;

        foreach (var data in datas)
        {
            (int l1, int r1, int l2, int r2) = (
                         int.Parse(data.L1),
                         int.Parse(data.R1),
                         int.Parse(data.L2),
                         int.Parse(data.R2)
                         );

            var contained = Contained(l1, r1, l2, r2);

            answer += contained ? 1 : 0;
        }

        return answer.ToString();
    }

    static bool Contained(int l1, int r1, int l2, int r2)
    {
        if (l1 >= l2 && r1 <= r2) return true;
        if (l2 >= l1 && r2 <= r1) return true;

        return false;
    }

}
