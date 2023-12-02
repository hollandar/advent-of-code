using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem08;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem08", RowRegex());
        RunPartB("Problem08", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    public struct TreeMap
    {
        public List<int> Trees;
        public int Width;
        public int Height;
    }

    protected TreeMap Load(IEnumerable<InputRow> datas)
    {
        var treeMap = new TreeMap() { Width = 0, Height = 0, Trees = new() };

        foreach (var line in datas)
        {
            DebugLn(line.Value);
            if (treeMap.Width == 0)
            {
                treeMap.Width = line.Value.Length;
            }
            else
            {
                if (treeMap.Width != line.Value.Length) throw new Exception();
            }

            treeMap.Trees.AddRange(line.Value.ToCharArray().Select(r => (int)(r - '0')));
            treeMap.Height++;
        }

        return treeMap;
    }
}

