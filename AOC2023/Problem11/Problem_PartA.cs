using AOCLib;
using System.IO.MemoryMappedFiles;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;

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
        // We are adding a new block within a pattern according to the group lengths, and varying the spacing to fit the pattern
        // eg ???.### 1,1,3
        //    #.#.###
        //    .#.#.### is too long, but is valid by the group counts, however the second '#' is detected as violating the . and the search ends there.

        // Final check the entire valid against its pattern;
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

            DebugLn(val);

            return 1;

        }
        var groupLength = groups[group];

        ulong options = 0;
        int start = val.Length;
        for (int i = 0; i < length - groupSum; i++)
        {
            int ex = val.Length + i + groupLength;
            if (groups.Length - 1 > group) ex++;
            if (ex > length)
            {
                continue;
            }

            // If this part of the pattern is going to make the entire match invalid we want to avoid
            // building it out as soon as possible.  Waiting until the end is too long.
            // That should be a final cross check only at that point.

            // Ensure any leading '.' are valid;
            bool invalid = false;
            for (int j = 0; j < i; j++)
            {
                if (pattern[start + j] == '#') invalid = true;
            }
            if (invalid) continue;

            // Ensure that the block of '#' will be valid;
            invalid = false;
            for (int j = i; j < groupLength; j++)
            {
                if (pattern[start + j] == '.') invalid = true;
            }
            if (invalid) continue;

            // Ensure that a trailing '.' will be valid (if there must be one)
            if (groups.Length - 1 > group)
            {
                if (pattern[start + i + groupLength] == '#') continue;
            }
            else if (length - ex - i - groupLength > 0)
            {
                // Ensure that any overflow (which will be '.' are also valid;
                invalid = false;
                for (int j = 0; j < length - ex - i - groupLength; j++)
                {
                    if (pattern[start + i + groupLength + j] == '#') invalid = true;
                }
                if (invalid) continue;
            }

            // Now extend the pattern, we know this part at least is valid;
            builder.Clear();
            builder.Append(val);
            builder.Append('.', i);

            builder.Append('#', groupLength);

            if (groups.Length - 1 > group)
                builder.Append('.');
            else if (length - builder.Length > 0)
                builder.Append('.', length - builder.Length);

            // And move into the next inner group to continue the pattern;
            options += MatchInner(pattern, groups, groupSum, length, group + 1, builder.ToString());

        }

        return options;
    }

    [GeneratedRegex("#+")]
    public static partial Regex SpringsRegex();

    public static int[] Configuration(string s) => SpringsRegex().Matches(s).Select(x => x.Length).ToArray();
}
