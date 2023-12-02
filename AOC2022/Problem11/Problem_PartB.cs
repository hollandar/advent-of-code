using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var monkeys = Load(datas);

        var dividends = monkeys.Values.Select(r => (r.Test as DivisibleByTest).By).Distinct();
        var adjustBy = dividends.Aggregate((x, y) => x * y);

        Print(".", indent: 2);
        for (int round = 0; round < 10000; round++)
        {
            MonkeyBusiness(monkeys, newConcern =>
            {
                while (newConcern > adjustBy)
                {
                    newConcern -= adjustBy;
                }

                return newConcern;
            });

            if ((round + 1) % 1000 == 0 || round == 0 || round == 19)
            {
                Print(".");
                DebugLn($"=== After round {round} ===");
                foreach (var currentMonkey in monkeys.Values.OrderBy(r => r.Inspected))
                {
                    DebugLn($"Monkey {currentMonkey.Id} inspected items {currentMonkey.Inspected} times.");
                }
            }

        }

        PrintLn(indent: 0);

        var total = monkeys.Values.OrderByDescending(r => r.Inspected).Take(2).Select(r => (long)r.Inspected).Aggregate((a, b) => a * b);
        return total.ToString();
    }

}
