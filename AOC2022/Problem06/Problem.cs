using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem06;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem06", RowRegex());
        RunPartB("Problem06", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    enum PhaseEnum { startOfPacket, startOfMessage };

    bool AllDifferent(IList<char> c)
    {
        for (int i = 0; i < c.Count; i++)
        {
            for (int j = 0; j < c.Count; j++)
            {
                if (i != j && c[i] == c[j])
                {
                    return false;
                }
            }
        }

        return true;
    }

}

