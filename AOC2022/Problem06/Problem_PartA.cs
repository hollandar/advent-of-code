using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem06;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var data = datas.Single().Value;
        var answer = -1;
        var chars = data.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            var signal = chars.Skip(i).Take(4).ToArray();
            if (AllDifferent(signal))
            {
                DebugLn($"start-of-packet: {i + 4}");
                if (answer == -1) answer = i + 4;
            }
        }

        return answer.ToString();
    }

}
