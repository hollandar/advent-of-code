namespace AOCLib.Parser
{
    public class WhitespaceMatch : Match
    {
        public WhitespaceMatch(string kind)
        {
            Kind = kind;
            Predicate = (s) =>
            {
                var digits = new string(s.TakeWhile(r => r == ' ' || r == '\t').ToArray());
                return digits.Length;
            };
        }
    }
}
