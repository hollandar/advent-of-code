using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem05;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);
        Stack<char>[] stacksArray = data.Stacks;

        DebugStack(stacksArray);


        foreach (var move in data.Moves)
        {

            int fromIndex = move.From - 1;
            int toIndex = move.To - 1;

            // Difference here is move via a temporary stack, so ending order is maintained

            var tempStack = new Stack<char>();
            for (int p = 0; p < move.Count; p++)
            {
                char moveChar = stacksArray[fromIndex].Pop();
                tempStack.Push(moveChar);
            }

            while (tempStack.Count > 0)
            {
                var moveChar = tempStack.Pop();
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
