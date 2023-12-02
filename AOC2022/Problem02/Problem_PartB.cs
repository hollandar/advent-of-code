using AOCLib;

namespace AdventOfCode2022.Problem02;

public partial class Problem : ProblemPart<InputRow>
{
    protected override long PartB(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        foreach (var data in datas)
        {
            var opponentPlay = data.Player1 switch
            {
                "A" => PlayEnum.Rock,
                "B" => PlayEnum.Paper,
                "C" => PlayEnum.Scissors,
                _ => throw new InvalidDataException()
            };

            var desiredOutcome = data.Player2 switch
            {
                "X" => OutcomeEnum.Loss,
                "Y" => OutcomeEnum.Draw,
                "Z" => OutcomeEnum.Win,
                _ => throw new InvalidDataException()
            };

            var myPlay = PlayToOutcome(opponentPlay, desiredOutcome);
            var roundScore = myPlay switch
            {
                PlayEnum.Rock => 1,
                PlayEnum.Paper => 2,
                PlayEnum.Scissors => 3,
                _ => throw new InvalidDataException()
            };

            var win = Win(myPlay, opponentPlay);
            roundScore += win switch
            {
                OutcomeEnum.Win => 6,
                OutcomeEnum.Draw => 3,
                OutcomeEnum.Loss => 0,
                _ => throw new InvalidDataException()
            };

            // Console.WriteLine($"{data.Player1} {data.Player2}  {opponentPlay} {myPlay} {roundScore}");

            answer += roundScore;
        }

        return answer;
    }

    static PlayEnum PlayToOutcome(PlayEnum opponentPlay, OutcomeEnum outcome)
    {
        if (outcome == OutcomeEnum.Win)
        {
            return opponentPlay switch
            {
                PlayEnum.Rock => PlayEnum.Paper,
                PlayEnum.Paper => PlayEnum.Scissors,
                PlayEnum.Scissors => PlayEnum.Rock
            };
        }

        if (outcome == OutcomeEnum.Loss)
        {
            return opponentPlay switch
            {
                PlayEnum.Rock => PlayEnum.Scissors,
                PlayEnum.Paper => PlayEnum.Rock,
                PlayEnum.Scissors => PlayEnum.Paper
            };
        }

        if (outcome == OutcomeEnum.Draw)
            return opponentPlay;

        throw new Exception();
    }

}
