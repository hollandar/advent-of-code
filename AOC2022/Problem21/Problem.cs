using AOCLib;
using System.Text.RegularExpressions;
using static AdventOfCode2022.Problem11.Problem;

namespace AdventOfCode2022.Problem21;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem21", RowRegex());
        RunPartB("Problem21", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    enum NodeType { Op, No }
    interface IMonkeyNode
    {
        NodeType Type { get; }
    }
    class MonkeyOp : IMonkeyNode
    {
        public string left;
        public string right;
        public string op;

        public NodeType Type { get { return NodeType.Op; } }
    }

    class MonkeyNo : IMonkeyNode
    {
        public long value;
        public NodeType Type { get { return NodeType.No; } }
    }

    static bool HasHuman(string node, IDictionary<string, IMonkeyNode> instructions)
    {
        if (node == "humn") return true;

        var monkeyNode = instructions[node];
        if (monkeyNode.Type == NodeType.Op)
        {
            return HasHuman(((MonkeyOp)monkeyNode).left, instructions) || HasHuman(((MonkeyOp)monkeyNode).right, instructions);
        }
        return false;
    }

    

    Dictionary<string, IMonkeyNode> Load(IEnumerable<InputRow> datas)
    {
        Dictionary<string, IMonkeyNode> instructions = new();
        var regexNo = new Regex(@"^(.{4}): (-?\d+)$");
        var regexOp = new Regex(@"^(.{4}): (.{4}) (.) (.{4})$");
        foreach (var data in datas)
        {
            var line = data.Value;
            var noMatch = regexNo.Match(line);
            if (noMatch.Success)
            {
                var monkeyNo = new MonkeyNo
                {
                    value = long.Parse(noMatch.Groups[2].Value)
                };
                instructions[noMatch.Groups[1].Value] = monkeyNo;
            }

            var opMatch = regexOp.Match(line);
            if (opMatch.Success)
            {
                var monkeyOp = new MonkeyOp
                {
                    left = opMatch.Groups[2].Value,
                    op = opMatch.Groups[3].Value,
                    right = opMatch.Groups[4].Value
                };
                instructions[opMatch.Groups[1].Value] = monkeyOp;
            }
        }

        return instructions;
    }
}

