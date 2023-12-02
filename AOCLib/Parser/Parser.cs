using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCLib.Parser
{

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
