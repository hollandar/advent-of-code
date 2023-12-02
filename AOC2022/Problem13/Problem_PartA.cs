using AOCLib;

namespace AdventOfCode2022.Problem13;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        long indexSum = 0;
        for (int i = 0; i < data.Count; i += 2)
        {
            var set = (i / 2) + 1;
            var list1 = data[i];
            var list2 = data[i + 1];

            DebugLn("==");
            DebugLn(list1.ToString());
            DebugLn(list2.ToString());
            DebugLn("==");

            var valid = Compare(list1, list2);
            if (valid == CompareEnum.Valid) indexSum += set;
            var validString = valid == CompareEnum.Valid ? "Valid" : "Invalid";
            DebugLn($"Pair {set} is {validString}");
        }

        return indexSum.ToString();
    }

}
