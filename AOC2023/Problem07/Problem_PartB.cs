using AOCLib;

namespace AdventOfCode2023.Problem07;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        var cardScores = datas.OrderBy(r => GetJokerScore(r.Cards)).ThenBy(r => r.Cards, new JokerHandComparer());

        for (int i = 1; i <= cardScores.Count(); i++)
        {
            var card = cardScores.ElementAt(i - 1);
            DebugLn($"{i} {card.Cards} {card.Bid}");
            answer += card.Bid * i;
        }

        return answer.ToString();
    }

    int GetJokerScore(string cards)
    {
        // By the rules of this game, taking the card you have most of with the highest face score and replicating it into the Jokers is the best strategy.
        // You could also have done this by brute force, but the search space is shortcut significantly by the above approach.

        var otherCards = cards.Where(r => r != 'J').ToArray();
        var jays = cards.Where(r => r == 'J').Count();
        if (jays > 0)
        {
            if (jays == 5)
            {
                // There is an instance that is all jokers, so we just make it the highest possible hand.
                // It doesnt seem like there are only 4 jokers in the deck, so presume 4 aces is ok.
                cards = "AAAAA";
            }
            else
            {
                // Rank the cards by which we have the most of and their card strength (which resolves the 1,2,2 scenario) and take the highest.
                var cardRanks = otherCards.GroupBy(c => c).Select(g => (g.Key, g.Count())).OrderBy(r => r.Item2).ThenBy(r => JokerHandComparer.GetCardStrength(r.Key));

                var highest = cardRanks.Last().Key;

                cards = cards.Replace('J', highest);
            }
        }

        var cardCounts = cards.ToArray().GroupBy(c => c).Select(g => g.Count()).OrderBy(r => r);

        var currentScore = cardCounts switch
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



        return currentScore;
    }






    public class JokerHandComparer : IComparer<string>
    {
        public static int GetCardStrength(char c)
        {
            return c switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 0,
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
