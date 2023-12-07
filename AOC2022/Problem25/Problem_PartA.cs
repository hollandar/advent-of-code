using AOCLib;

namespace AdventOfCode2022.Problem25;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        Assert(1 == FromSnafu("1"));
        Assert(2 == FromSnafu("2"));
        Assert(3 == FromSnafu("1="));
        Assert(4 == FromSnafu("1-"));
        Assert(5 == FromSnafu("10"));
        Assert(314159265 == FromSnafu("1121-1110-1=0"));
        Assert("1" == ToSnafu(1));
        Assert("2" == ToSnafu(2));
        Assert("1=" == ToSnafu(3));
        Assert("1-" == ToSnafu(4));
        Assert("10" == ToSnafu(5));
        Assert("1121-1110-1=0" == ToSnafu(314159265));

        long total = 0;
        foreach (var data in datas)
        {
            var line = data.Value;
            var value = FromSnafu(line);
            total += value;

            DebugLn($"{line} = {value}");
        }

        DebugLn("Sum");
        DebugLn($"{ToSnafu(total)} = {total}");


        return ToSnafu(total);
    }

}
