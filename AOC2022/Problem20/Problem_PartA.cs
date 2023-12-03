using AOCLib;

namespace AdventOfCode2022.Problem20;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        Test();
        List<long> numbers = new();
        foreach (var data in datas)
        {
            var line = data.Value;
            var n = long.Parse(line);
            numbers.Add(n);
        }

        var numbersArray = new ArrayBlock(numbers.ToArray());
        foreach (var n in numbersArray.Values) Debug(n + " ");
        for (int i = 0; i < numbers.Count; i++)
        {
            (var index, var value) = numbersArray.IndexOfPosition(i);
            numbersArray.Move(index, value.Value);

        }
        foreach (var n in numbersArray.Values) Debug(n + " ");
        DebugLn();

        var one = numbersArray.NthAfter(1000, 0);
        DebugLn($"1000th after 0: {one}");
        var two = numbersArray.NthAfter(2000, 0);
        DebugLn($"2000th after 0: {two}");
        var three = numbersArray.NthAfter(3000, 0);
        DebugLn($"3000th after 0: {three}");
        return (one + two + three).ToString();
    }

}
