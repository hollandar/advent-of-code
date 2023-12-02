﻿using AOCLib;

namespace AdventOfCode2023.Problem01;

public partial class Problem : ProblemPart<InputRow>
{
    protected override long PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        foreach (var data in datas)
        {
            var v = data.Value;

            var numbers = Enumerable.Range(0, v.Length).Select(x => v.Substring(x)).Select(x => {
                if (x.StartsWith("1")) { return 1; }
                if (x.StartsWith("2")) { return 2; }
                if (x.StartsWith("3")) { return 3; }
                if (x.StartsWith("4")) { return 4; }
                if (x.StartsWith("5")) { return 5; }
                if (x.StartsWith("6")) { return 6; }
                if (x.StartsWith("7")) { return 7; }
                if (x.StartsWith("8")) { return 8; }
                if (x.StartsWith("9")) { return 9; }
                return -1;
            }).Where(r => r != -1);


            var first = numbers.First();
            var last = numbers.Last();

            answer += first * 10 + last;
        }

        return answer;
    }

}
