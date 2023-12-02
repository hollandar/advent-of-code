using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem10;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        var interestingCycles = new HashSet<int> { 20, 60, 100, 140, 180, 220 };
        var signalStrengths = new List<int>();
        int cycle = 0;
        int regX = 1;
        foreach (var operation in data)
        {
            Action? applyOperation = null;
            int cycleLength = 0;
            switch (operation.Opp)
            {
                case OppEnum.Noop:
                    cycleLength = 1;
                    applyOperation = () => { };
                    break;
                case OppEnum.Addx:
                    cycleLength = 2;
                    applyOperation = () =>
                    {
                        regX += operation.IntArg0.Value;
                    };
                    break;
            }

            while (cycleLength > 0)
            {
                cycleLength--;
                cycle++;

                if (interestingCycles.Contains(cycle))
                {
                    int signalStrength = cycle * regX;
                    signalStrengths.Add(signalStrength);
                }


            }

            if (applyOperation != null)
            {
                applyOperation();
            }
        }

        return signalStrengths.Sum().ToString();
    }

}
