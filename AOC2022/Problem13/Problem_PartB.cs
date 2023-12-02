using AOCLib;

namespace AdventOfCode2022.Problem13;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var list = Load(datas);
        var markerStart = ParseList("[[2]]").Item2;
        var markerEnd = ParseList("[[6]]").Item2;
        list.Add(markerStart);
        list.Add(markerEnd);

        list.Sort();

        int markerStartIndex = 0, markerEndIndex = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Compare(list[i], markerStart) == 0)
            {
                markerStartIndex = i + 1;
                DebugLn($"Start index = {i + 1}");
            }
            if (Compare(list[i], markerEnd) == 0)
            {
                markerEndIndex = i + 1;
                DebugLn($"End index = {i + 1}");
            }
        }

        return (markerStartIndex * markerEndIndex).ToString();
    }

}
