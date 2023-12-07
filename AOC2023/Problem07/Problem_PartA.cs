using AOCLib;

namespace AdventOfCode2023.Problem07;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        var cardScores = datas.OrderBy(r => GetScore(r.Cards)).ThenBy(r => r.Cards, new HandComparer());
        
        for (int i = 1; i <= cardScores.Count(); i++)
        {
            var card = cardScores.ElementAt(i - 1);
            DebugLn($"{i} {card.Cards} {card.Bid}");
            answer += card.Bid * i;
        }

        return answer.ToString();
    }

    int GetScore(string cards)
    {
        var cardCounts = cards.ToArray().GroupBy(c => c).Select(g => g.Count()).OrderBy(r => r);

        return cardCounts switch
        {
            { } when cardCounts.SequenceEqual([5]) => 7000,
            { } when cardCounts.SequenceEqual([1, 4]) => 6000,
            { } when cardCounts.SequenceEqual([2, 3]) => 5000,
            { } when cardCounts.SequenceEqual([1, 1, 3]) => 4000,
            { } when cardCounts.SequenceEqual([1, 2, 2]) => 3000,
            { } when cardCounts.SequenceEqual([1, 1, 1, 2]) => 2000,
            { } when cardCounts.SequenceEqual([1, 1, 1, 1, 1]) => 1000,
            _ => 0
        };
    }
    
    // This comparer deals with the fact that KAAAA is lower than AAAAA.
    // Of course this rule is odd, but so be it.
    public class HandComparer : IComparer<string>
    {
        public static int GetCardStrength(char c)
        {
            return c switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => int.Parse(c.ToString())
            };
        }

        public int OrderHands(string hand1, string hand2)
        {
            for (int i = 0; i < Math.Min(hand1.Length, hand2.Length); i++)
            {
                if (hand1[i] == hand2[i])
                    continue;

                if (GetCardStrength(hand1[i]) > GetCardStrength(hand2[i]))
                    return 1;
                else
                    return -1;
            }

            return 0;
        }

        public int Compare(string? x, string? y)
        {
            return OrderHands(x, y);
        }
    }
}


