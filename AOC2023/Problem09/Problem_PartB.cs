using AOCLib;

namespace AdventOfCode2023.Problem09;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        long answer = 0;
        foreach (var data in datas)
        {
            Stack<IEnumerable<long>> sequences = new();
            IEnumerable<long> sequence = data.Value.Split(" ").Select(long.Parse);
            sequences.Push(sequence);

            var sum = sequence.Sum();

            while (sum != 0)
            {
                sequence = DifferencesSequence(sequence);
                sequences.Push(sequence);
                sum = sequence.Sum();
            }

            // This sequence is all zeros
            sequence = sequences.Pop();

            long additionToPrevious = 0;
            while (sequences.Count > 0)
            {
                sequence = sequences.Pop();
                additionToPrevious = sequence.First() - additionToPrevious;
            }

            answer += additionToPrevious;
        }

        return answer.ToString();
    }

}
