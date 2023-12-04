using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem21;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        long answer = 0;

        Dictionary<string, IMonkeyNode> instructions = Load(datas);

        answer = Calc("root", instructions);
        return answer.ToString();
    }

    static long Calc(string node, IDictionary<string, IMonkeyNode> instructions)
    {
        var monkeyNode = instructions[node];
        if (monkeyNode.Type == NodeType.No)
        {
            return ((MonkeyNo)monkeyNode).value;
        }
        else if (monkeyNode.Type == NodeType.Op)
        {
            var opNode = (MonkeyOp)monkeyNode;
            var left = Calc(opNode.left, instructions);
            var right = Calc(opNode.right, instructions);

            switch (opNode.op)
            {
                case "+": return left + right;
                case "-": return left - right;
                case "/": return left / right;
                case "*": return left * right;
                default: throw new Exception();
            }

        }
        else
        {
            throw new Exception();
        }
    }

}
