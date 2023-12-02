using AOCLib;

namespace AdventOfCode2022.Problem03;

public partial class Problem : ProblemPart<InputRow>
{
    protected override long PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;

        foreach (var data in datas)
        {
            var line = data.Value;
            char[] pack = line.ToArray();
            char[] left = pack.Take(pack.Length / 2).ToArray();
            char[] right = pack.Skip(pack.Length / 2).ToArray();

            char error = left.Intersect(right).Single();
            int score = CalculateScore(error);

            answer += score;
        }

        return answer;
    }

    static int CalculateScore(char character)
    {
        return Char.IsUpper(character) ? (character - 'A') + 27 : (character - 'a') + 1;
    }

}
