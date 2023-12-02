using AOCLib;

namespace AdventOfCode2022.Problem02;

public partial class Problem : ProblemPart<InputRow>
{
    protected override long PartA(IEnumerable<InputRow> datas)
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

            var myPlay = data.Player2 switch
            {
                "X" => PlayEnum.Rock,
                "Y" => PlayEnum.Paper,
                "Z" => PlayEnum.Scissors,
                _ => throw new InvalidDataException()
            };

            var outcome = Win(myPlay, opponentPlay);
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


    static OutcomeEnum Win(PlayEnum myPlay, PlayEnum opponentPlay)
    {
        if (myPlay == PlayEnum.Rock && opponentPlay == PlayEnum.Scissors) { return OutcomeEnum.Win; }
        if (myPlay == PlayEnum.Scissors && opponentPlay == PlayEnum.Paper) { return OutcomeEnum.Win; }
        if (myPlay == PlayEnum.Paper && opponentPlay == PlayEnum.Rock) { return OutcomeEnum.Win; }

        if (myPlay == opponentPlay) return OutcomeEnum.Draw;

        return OutcomeEnum.Loss;
    }
}
