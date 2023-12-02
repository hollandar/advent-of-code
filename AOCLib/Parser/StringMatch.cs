namespace AOCLib.Parser
{
    public class StringMatch : Match
    {
        public StringMatch(string kind, params string[] possibleValues)
        {
            Kind = kind;
            Predicate = (s) =>
            {
                foreach (var value in possibleValues)
                {
                    if (s.StartsWith(value))
                    {
                        return value.Length;
                    }
                }
                return 0;
            };
        }
    }
}
