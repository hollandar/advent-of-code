using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem05;

public partial class Problem : ProblemPart<InputRow>
{
    

    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);
        Stack<char>[] stacksArray = data.Stacks;

        DebugStack(stacksArray);


        foreach (var move in data.Moves)
        {

            int fromIndex = move.From - 1;
            int toIndex = move.To - 1;

            var tempStack = new Stack<char>();
            for (int p = 0; p < move.Count; p++)
            {
                char moveChar = stacksArray[fromIndex].Pop();
                stacksArray[toIndex].Push(moveChar);
            }

            DebugStack(stacksArray);
        }

        var answer = string.Empty;
        foreach (var stack in stacksArray)
        {
            if (stack.TryPeek(out var item))
                answer += (item.ToString());
            else
                answer += ("_");
        }

        return answer;
    }

}
