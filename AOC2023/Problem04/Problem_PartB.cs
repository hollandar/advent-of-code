using AOCLib;

namespace AdventOfCode2023.Problem04;

public partial class Problem : ProblemPart<InputRow>
{
    class Card
    {
        public int CardNumber { get; set; }
        public HashSet<int> WinningNumbers { get; set; }
        public HashSet<int> MyNumbers { get; set; }
        public int WinningCount { get; set; }
        public int Copies { get; internal set; }
    }

    protected override string PartB(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        var cards = new Dictionary<int, Card>();

        foreach (var data in datas)
        {
            var matches = NumberRegex().Matches(data.Value);
            var matchValues = matches.Select(r => r.Value);
            var cardNumber = int.Parse(matchValues.First());
            var winningNumbers = matchValues.Skip(1).TakeWhile(r => r != "|").Select(r => int.Parse(r)).ToHashSet();
            var myNumbers = matchValues.SkipWhile(r => r != "|").Skip(1).Select(r => int.Parse(r)).ToHashSet();

            var winningCount = winningNumbers.Intersect(myNumbers).Count();
            var value = winningCount > 0 ? 1 : 0;
            for (int i = 1; i < winningCount; i++)
            {
                value = value * 2;
            }

            cards[cardNumber] = new Card
            {
                CardNumber = cardNumber,
                WinningNumbers = winningNumbers,
                MyNumbers = myNumbers,
                WinningCount = winningCount,
                Copies = 0
            };  
        }

        var currentCard = 1;
        while (currentCard <= cards.Keys.Max())
        {
            var card = cards[currentCard];

            for (int j = 0; j <= card.Copies; j++) // <= because we need to count the current card
            {
                for (int i = 0; i < card.WinningCount; i++)
                {
                    cards[currentCard + i + 1].Copies++;
                }
            }

            answer += (card.Copies + 1);
            currentCard++;
        }

        
        
        return answer.ToString();
    }

}
