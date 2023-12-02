using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem10;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem10", RowRegex());
        RunPartB("Problem10", RowRegex());
    }

    [GeneratedRegex("^(?<Op>.*?)(?:$|\\s)(?<IntArg>(?:-|\\d){0,})$")]
    public static partial Regex RowRegex();

    public struct Operation
    {
        public OppEnum Opp;
        public int? IntArg0;
    }

    public enum OppEnum { Addx, Noop }


    protected List<Operation> Load(IEnumerable<InputRow> datas)
    {
        var operations = new List<Operation>();
        foreach (var data in datas)
        {
            var opString = data.Op;
            var numberString = data.IntArg;

            var op = Enum.Parse<OppEnum>(opString, true);
            int? number = null;
            if (int.TryParse(numberString, out var n)) { number = n; }

            switch (op)
            {
                case OppEnum.Noop:
                    System.Diagnostics.Debug.Assert(number == null);

                    break;
                case OppEnum.Addx:
                    System.Diagnostics.Debug.Assert(number != null); break;
            }

            operations.Add(new Operation { Opp = op, IntArg0 = number });
        }

        return operations;

    }
}

