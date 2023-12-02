namespace AOCLib.Parser
{
    public class IntegerMatch : Match
    {
        public IntegerMatch(string kind)
        {
            Kind = kind;
            Predicate = (s) =>
            {
                var digits = new string(s.TakeWhile(r => char.IsDigit(r)).ToArray());
                return digits.Length;
            };
        }
    }
}
