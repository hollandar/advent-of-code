using AOCLib;

namespace AdventOfCode2022.Problem04;

public partial class Problem : ProblemPart<InputRow>
{
    protected override long PartB(IEnumerable<InputRow> datas)
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
            var overlaps = Overlaps(l1, r1, l2, r2);

            if (contained && !overlaps)
                throw new Exception();

            answer += overlaps ? 1 : 0;
        }

        return answer;
    }

    static bool Overlaps(int l1, int r1, int l2, int r2)
    {
        var e1 = Enumerable.Range(l1, r1 - l1 + 1);
        var e2 = Enumerable.Range(l2, r2 - l2 + 1);
        return e1.Intersect(e2).Any();
    }

}
