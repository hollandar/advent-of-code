using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem02;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem02", RowRegex());
        RunPartB("Problem02", RowRegex());
    }

    [GeneratedRegex("^(?<Player1>A|B|C)\\s(?<Player2>X|Y|Z)$")]
    public static partial Regex RowRegex();

    enum PlayEnum { Rock, Paper, Scissors }
    enum OutcomeEnum { Win, Loss, Draw }

}

