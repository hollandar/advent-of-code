using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem10;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        var interestingCycles = new HashSet<int> { 20, 60, 100, 140, 180, 220 };
        int cycle = 0;
        int regX = 1;
        Print(indent: 2);

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
                int scanLine = (cycle / 40) * 40;
                int spriteMin = scanLine + (regX - 1);
                int spriteMax = scanLine + (regX + 1);
                if (cycle % 40 == 0)
                {
                    PrintLn(indent: 0);
                    Print(indent: 2);
                }

                if (cycle >= spriteMin && cycle <= spriteMax)
                    Print("#");
                else
                    Print(".");

                cycleLength--;
                cycle++;
            }

            if (applyOperation != null)
            {
                applyOperation();
            }
        }

        PrintLn(indent: 0);

        return string.Empty;
    }

}
