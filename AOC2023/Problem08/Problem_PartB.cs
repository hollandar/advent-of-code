using AOCLib;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem08;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        ulong answer = 0;
        var instructions = datas.First().Value;
        var lrLregex = new Regex("^(...) = [(](...), (...)[)]$");
        var moves = new Dictionary<int, (int, int)>();
        HashSet<int> endingWithZ = new HashSet<int>();
        List<int> endingWithA = new List<int>();
        int[] leftOf = new int[150000];
        int[] rightOf = new int[150000];
        foreach (var data in datas.Skip(2))
        {
            var match = lrLregex.Match(data.Value);
            if (match.Success)
            {
                leftOf[StringToIndex(match.Groups[1].Value)] = StringToIndex(match.Groups[2].Value);
                rightOf[StringToIndex(match.Groups[1].Value)] = StringToIndex(match.Groups[3].Value);

                if (match.Groups[1].Value.EndsWith('A')) endingWithA.Add(StringToIndex(match.Groups[1].Value));
                if (match.Groups[1].Value.EndsWith('Z')) endingWithZ.Add(StringToIndex(match.Groups[1].Value));
            }
        }

        // This was optimized to use vectors for the cells, bit it is still too slow.
        var endingWithArray = new int[Vector<int>.Count];
        endingWithA.CopyTo(endingWithArray);
        Vector<int> locations = new Vector<int>(endingWithArray);

        var left = new int[8];
        var right = new int[8];
        Vector<int> negative = new Vector<int>(new int[] { -1, -1, -1, -1, -1, -1, -1, -1 });
        var i = 0;
        var zed = StringToIndex("00Z");
        ulong[] last = new ulong[endingWithA.Count];
        ulong[] interval = new ulong[endingWithA.Count];
        do
        {
            var move = instructions[i];
            i++;
            if (i == instructions.Length) i = 0;

            Vector<int> conditional;

            if (move == 'L')
            {
                conditional = negative;
            }
            else
            {
                conditional = Vector<int>.Zero;
            }

            for (var j = 0; j < endingWithA.Count; j++)
            {
                left[j] = leftOf[locations[j]];
                right[j] = rightOf[locations[j]];
            }

            var leftVector = new Vector<int>(left);
            var rightVector = new Vector<int>(right);

            locations = Vector.ConditionalSelect(conditional, leftVector, rightVector);

            answer++;
            var allZeds = true;
            for (var k = 0; k < endingWithA.Count; k++)
            {
                if ((locations[k] & zed) != zed)
                {
                    allZeds = false;
                }
                else
                {
                    interval[k] = answer - last[k];
                    last[k] = answer;
                }
            }

            if (allZeds || interval.All(r => r > 0)) break;

        } while (true);

        // So just get the intervals and find the lowest interval that is devisable by all intervals.
        answer = interval[0];
        while (!interval.All(r => r == 0 || answer % r == 0))
        {
            answer += interval[0];
        }

        return answer.ToString();
    }

    char[] indexes = new char[]
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C','D','E','F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };


    // Put the three character strings into a single 32 bit integer so we can use vectors for calculations.
    int StringToIndex(string s)
    {
        int p = 0;
        for (int i = 0; i < s.Length; i++)
        {
            int ix = 0;
            for (int j = 0; j < indexes.Length; j++)
            {
                if (indexes[j] == s[i]) ix = j;
            }
            p |= (ix << i * 6);
        }

        return p;
    }

}
