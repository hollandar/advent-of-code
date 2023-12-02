using AOCLib;
using AOCLib.Parser;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problem02;

public partial class Problem : ProblemPart<InputRow>
{
    protected override long PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;


        foreach (var data in datas)
        {
            var match = GameRegex().Match(data.Value);
            Debug.Assert(match.Success);
            var gameNumber = int.Parse(match.Groups["GameNumber"].Value);

            var throws = match.Groups["Throws"].Value;

            var possible = true;

            var tokens = Parser.Parse(throws, [
                new IntegerMatch("Count"),
                new StringMatch("Color", "red", "green", "blue"),
                new CharacterMatch("Comma", ','),
                new CharacterMatch("SemiColon", ';'),
                new WhitespaceMatch("Whitespace"),
                ]);

            var state = 0;

            int count = 0;
            string color = string.Empty;

            int red = 12;
            int green = 13;
            int blue = 14;

            var check = () =>
            {
                switch (color)
                {
                    case "red":
                        possible &= count <= red;
                        state = 0;
                        break;
                    case "green":
                        possible &= count <= green;
                        state = 0;
                        break;
                    case "blue":
                        possible &= count <= blue;
                        state = 0;
                        break;
                    default:
                        throw new Exception("Unexpected color.");
                }
            };

            foreach (var token in tokens)
            {
                if (token.Kind == "Whitespace") continue;
                if (token.Kind == "EndOfLine")
                {
                    check();
                    break;
                }

                switch (state)
                {
                    case 0:
                        // Expecting count
                        switch (token.Kind)
                        {
                            case "Count":
                                count = int.Parse(token.Value);
                                state = 1;
                                break;
                            default:
                                throw new Exception($"Unexpected token {token.Kind} at state {state}");
                        }
                        break;
                    case 1:
                        // Expecting color
                        switch (token.Kind)
                        {
                            case "Color":
                                color = token.Value;
                                state = 2;
                                break;
                            default:
                                throw new Exception($"Unexpected token {token.Kind} at state {state}");
                        }
                        break;
                    case 2:
                        // Expecting comma or semicolon
                        switch (token.Kind)
                        {
                            case "Comma":
                                check();
                                state = 0;
                                break;
                            case "SemiColon":
                                check();
                                state = 0;
                                break;
                            default:
                                throw new Exception($"Unexpected token {token.Kind} at state {state}");
                        }
                        break;
                }
            }

            if (possible)
            {
                answer += gameNumber;
            }
        }

        return answer;
    }

    [GeneratedRegex("^Game (?<GameNumber>\\d+): (?<Throws>.*)$")]
    public partial Regex GameRegex();

    [GeneratedRegex("(?:(?<Count>\\d+) (?<Color>blue|red|green),{0,1})*;")]
    public partial Regex ThrowsRegex();
}
