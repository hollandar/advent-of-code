using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var monkeys = Load(datas);

        for (int round = 0; round < 20; round++)
        {
            MonkeyBusiness(monkeys, newConcern => newConcern / 3);

            DebugLn($"=== After round {round} ===");
            foreach (var currentMonkey in monkeys.Values.OrderBy(r => r.Inspected))
            {
                DebugLn($"Monkey {currentMonkey.Id} inspected items {currentMonkey.Inspected} times.");
            }
        }

        var total = monkeys.Values.OrderByDescending(r => r.Inspected).Take(2).Select(r => r.Inspected).Aggregate((a, b) => a * b);

        return total.ToString();
    }
}

