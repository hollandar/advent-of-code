using AOCLib;

namespace AdventOfCode2022.Problem03;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        int answer = 0;

        var elfGroup = new List<char[]>();
        foreach (var data in datas)
        {
            var line = data.Value;
            char[] pack = line.ToArray();
            elfGroup.Add(pack);

            if (elfGroup.Count == 3)
            {
                var badge = elfGroup[0].Intersect(elfGroup[1]).Intersect(elfGroup[2]).Single();
                var badgeScore = CalculateScore(badge);

                elfGroup.Clear();

                answer += badgeScore;
            }
        }

        return answer.ToString();
    }

}
