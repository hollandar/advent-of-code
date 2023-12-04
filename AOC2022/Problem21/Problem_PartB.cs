using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem21;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        long answer = 0;

        Dictionary<string, IMonkeyNode> instructions = Load(datas);

        var node = (MonkeyOp)instructions["root"];

        if (HasHuman(node.left, instructions)) {
            var right = Calc(node.right, instructions);
            answer = UnCalc(node.left, right, instructions);
        }
        else
        {
            var left = Calc(node.left, instructions);
            answer = UnCalc(node.right, left, instructions);
        }

        return answer.ToString();
    }

    static long UnCalc(string node, long result, IDictionary<string, IMonkeyNode> instructions)
    {
        var monkeyNode = instructions[node];
        if (node == "humn")
        {
            return result;
        }
        else
        if (monkeyNode.Type == NodeType.No)
        {
            throw new Exception();
        }
        else if (monkeyNode.Type == NodeType.Op)
        {
            var opNode = (MonkeyOp)monkeyNode;
            if (HasHuman(opNode.left, instructions))
            {
                var right = Calc(opNode.right, instructions);
                long lower;
                switch (opNode.op)
                {
                    case "+": lower = result - right; break;
                    case "-": lower = result + right; break;
                    case "/": lower = result * right; break;
                    case "*": lower = result / right; break;
                    default: throw new Exception();
                }

                return UnCalc(opNode.left, lower, instructions);
            }
            else
            {
                var left = Calc(opNode.left, instructions);
                long lower;
                switch (opNode.op)
                {
                    case "+": lower = result - left; break;
                    case "-": lower = left - result; break;
                    case "/": lower = result * left; break;
                    case "*": lower = result / left; break;
                    default: throw new Exception();
                }

                return UnCalc(opNode.right, lower, instructions);
            }
        }
        else
        {
            throw new Exception();
        }
    }

}
