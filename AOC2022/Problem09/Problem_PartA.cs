using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem09;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        SetDebug();
        var data = Load(datas);

        var head = new Head();
        var tail = new Tail();

        foreach (var move in data)
        {
            for (int m = 0; m < move.Distance; m++)
            {
                head.Move(new Move(move.Direction, 1));
                tail.MoveRelativeTo(head);
            }

        }

        return tail.VisitedPoints.Count.ToString();
    }

}
