using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem05;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem05", RowRegex());
        RunPartB("Problem05", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    record NumberRange(ulong To, ulong From, ulong Count)
    {
        public static NumberRange FromArray(ulong[] values)
        {
            return new NumberRange(values[0], values[1], values[2]);
        }

        public bool InRange(ulong value)
        {
            return value >= From && value < From + Count;
        }

        public ulong Value(ulong value)
        {
            if (!InRange(value)) throw new Exception();
            return (value - From) + To;
        }
    }
}

