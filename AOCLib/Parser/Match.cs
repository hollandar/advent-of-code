namespace AOCLib.Parser
{
    public class Match
    {
        public Match() { }
        public Match(string kind, Func<string, int> predicate)
        {
            Kind = kind;
            Predicate = predicate;
        }

        public Func<string, int> Predicate { get; set; } = (s) => -1;
        public string Kind { get; set; } = string.Empty;
    }
}
