using AOCLib;
using System.IO.MemoryMappedFiles;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        ulong answer = 0;
        foreach (var data in datas)
        {
            var springs = data.Springs;
            var brokenGroups = data.Numbers.Split(',').Select(x => int.Parse(x)).ToArray();

            var variations = Match(springs, brokenGroups.ToArray());
            DebugLn($"RESULT: {springs} {variations}");

            answer += variations;
        }

        return answer.ToString();
    }

    ulong Match(string pattern, int[] groups)
    {
        var length = pattern.Length;
        var groupSum = groups.Sum();
        return MatchInner(pattern, groups, groupSum, length, 0);
    }

    static StringBuilder builder = new();
    ulong MatchInner(string pattern, int[] groups, int groupSum, int length, int group, string val = "")
    {
        if (group == groups.Length)
        {
            if (val.Length != length)
            {
                return 0;
            }

            for (var i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '#' && val[i] != '#' || pattern[i] == '.' && val[i] != '.')
                {
                    return 0;
                }
            }

            //Console.WriteLine(val);

            return 1;

        }
        var groupLength = groups[group];

        ulong options = 0;
        for (int i = 0; i < length - groupSum; i++)
        {
            int ex = val.Length + i + groupLength;
            if (groups.Length - 1 > group) ex++;
            if (ex > length)
            {
                continue;
            }
            builder.Clear();
            builder.Append(val);
            builder.Append('.', i);
            builder.Append('#', groupLength);
            if (groups.Length - 1 > group)
                builder.Append('.');
            else if (length - builder.Length > 0)
                builder.Append('.', length - builder.Length);

            options += MatchInner(pattern, groups, groupSum, length, group + 1, builder.ToString());

        }

        return options;
    }

    [GeneratedRegex("#+")]
    public static partial Regex SpringsRegex();

    public static int[] Configuration(string s) => SpringsRegex().Matches(s).Select(x => x.Length).ToArray();
}
