using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem06;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var data = datas.Single().Value;

        var answer = -1;
        var chars = data.ToCharArray();
        var phase = PhaseEnum.startOfPacket;
        for (int i = 0; i < chars.Length; i++)
        {
            if (phase == PhaseEnum.startOfPacket)
            {
                var signal = chars.Skip(i).Take(4).ToArray();
                if (AllDifferent(signal))
                {
                    DebugLn($"start-of-packet: {i + 4}");
                    phase = PhaseEnum.startOfMessage;
                }
            }
            if (phase == PhaseEnum.startOfMessage)
            {
                var signal = chars.Skip(i).Take(14).ToArray();
                if (AllDifferent(signal))
                {
                    DebugLn($"start-of-message: {i + 14}");
                    if (answer == -1) answer = i + 14;
                    break;
                }
            }
        }

        return answer.ToString();
    }

}
