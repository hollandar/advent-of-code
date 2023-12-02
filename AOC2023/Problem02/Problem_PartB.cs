using AOCLib;
using AOCLib.Parser;
using System.Diagnostics;

namespace AdventOfCode2023.Problem02;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        long answer = 0;


        foreach (var data in datas)
        {
            var match = GameRegex().Match(data.Value);
            Assert(match.Success);
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

            int red = 0;
            int green = 0;
            int blue = 0;

            var check = () =>
            {
                switch (color)
                {
                    case "red":
                        red = Math.Max(red, count);
                        state = 0;
                        break;
                    case "green":
                        green = Math.Max(green, count);
                        state = 0;
                        break;
                    case "blue":
                        blue = Math.Max(blue, count);
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
                answer += red * green * blue;
            }

        }

        return answer.ToString();
    }
}
