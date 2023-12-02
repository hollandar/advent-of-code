using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCLib.Parser
{

    public class EndOfLineMatch : Match
    {
        public EndOfLineMatch(string kind)
        {
            Kind = kind;
            Predicate = (s) =>
            {
                return s.Length > 0 ? 0 : 1;
            };
        }
    }

    public class CharacterMatch : Match
    {
        public CharacterMatch(string kind, params char[] possibleValues)
        {
            Kind = kind;
            Predicate = (s) =>
            {
                foreach (var value in possibleValues)
                {
                    if (s.StartsWith(value))
                    {
                        return 1;
                    }
                }
                return 0;
            };
        }
    }

    public class Token
    {
        public string Kind { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public static class Parser
    {
        public static IEnumerable<Token> Parse(string input, IEnumerable<Match> matches)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var slice = input.Substring(i);
                foreach (var match in matches)
                {
                    var value = match.Predicate(slice.ToString());
                    if (value > 0)
                    {
                        yield return new Token() { Kind = match.Kind, Value = input.Substring(i, value).ToString() };
                        i += value - 1;
                        break;
                    }
                }
            }

            yield return new Token() { Kind = "EndOfLine", Value = string.Empty };
        }
    }
}
