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
}
