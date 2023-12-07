using AOCLib;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem25;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem25", RowRegex());
        RunPartB("Problem25", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    static long FromSnafu(string s)
    {
        List<char> chars = new List<char> { '=', '-', '0', '1', '2' };
        long total = 0;
        for (int i = 0; i < s.Length; i++)
        {
            var position = s.Length - 1 - i;
            var value = chars.IndexOf(s[i]) - 2;

            total += ((long)Math.Pow(5, position)) * value;
        }

        return total;
    }

    static string ToSnafu(long l)
    {
        List<char> chars = new List<char> { '0', '-', '0', '1', '2' };
        StringBuilder result = new();
        int position = 1;
        int carry = 0;
        while (l > 0 || carry > 0)
        {
            var positionValue = (long)Math.Pow(5, position);
            var subpositionValue = positionValue / 5;

            var remainder = (l % positionValue) + (carry * subpositionValue);
            int carryNext = 0;
            if (remainder == 0)
            {
                result.Insert(0, "0");
            }
            else if (remainder == subpositionValue)
            {
                result.Insert(0, "1");
            }
            else if (remainder == subpositionValue * 2)
            {
                result.Insert(0, "2");
            }
            else if (remainder == subpositionValue * 3)
            {
                result.Insert(0, "=");
                carryNext++;
            }
            else if (remainder == subpositionValue * 4)
            {
                result.Insert(0, "-");
                carryNext++;
            }
            else if (remainder == subpositionValue * 5)
            {
                result.Insert(0, "0");
                carryNext++;
            }
            else throw new Exception();

            l -= (l % positionValue);

            position++;
            carry = carryNext;
        }

        return result.ToString();
    }
}

