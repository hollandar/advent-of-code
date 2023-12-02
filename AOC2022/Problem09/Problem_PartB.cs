using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem09;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        const int tailLength = 9;

        var head = new Head();
        var tails = new List<Tail>(tailLength);
        for (; tails.Count < tails.Capacity;)
        {
            tails.Add(new Tail());
        }


        foreach (var move in data)
        {
            for (int m = 0; m < move.Distance; m++)
            {
                head.Move(new Move(move.Direction, 1));
                tails[0].MoveRelativeTo(head);
                for (int i = 1; i < tails.Count; i++)
                {
                    tails[i].MoveRelativeTo(tails[i - 1]);
                }
            }
        }

        return tails.Last().VisitedPoints.Count.ToString();
    }

}
