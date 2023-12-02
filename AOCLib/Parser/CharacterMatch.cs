namespace AOCLib.Parser
{
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
}
