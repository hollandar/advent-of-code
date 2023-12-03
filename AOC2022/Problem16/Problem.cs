using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem16;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem16", RowRegex());
        RunPartB("Problem16", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    class Valve : IComparable
    {
        public string id;
        public int rate;
        public List<Edge> edges = new();
        public HashSet<string> leadsToId = new();
        public ulong mask = 0;

        public int CompareTo(object? obj)
        {
            return (obj as Valve)?.id.CompareTo(this.id) ?? -1;
        }

        public override string ToString()
        {
            return $"Valve {id} rate {rate} leads to {String.Join(", ", edges.Select(r => r.valve.id))}";
        }

        public override bool Equals(object? obj)
        {
            return (obj as Valve)?.id.Equals(this.id) ?? false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }

    class Edge
    {
        public Valve valve;
    }

    [GeneratedRegex(@"^Valve (.{2}) has flow rate=(\d+); tunnel(?:s){0,1} lead(?:s){0,1} to valve(?:s){0,1} (?:(.{2})(?:, |$)){0,}")]
    protected partial Regex LoadRegex();

    IDictionary<string, Valve> Load(IEnumerable<InputRow> datas)
    {
        Dictionary<string, Valve> valves = new Dictionary<string, Valve>();
        int ix = 0;
        foreach (var data in datas)
        {
            var line = data.Value;
            var match = LoadRegex().Match(line);

            if (match.Success)
            {
                var id = match.Groups[1].Value;
                var rate = int.Parse(match.Groups[2].Value);
                var connected = match.Groups[3].Captures.Select(c => c.Value).ToHashSet();
                valves.Add(id, new Valve { mask = (ulong)1 << ix++, id = id, rate = rate, leadsToId = connected });
            }
        }

        if (valves.Values.Count != valves.Values.Select(r => r.mask).Distinct().Count())
            throw new Exception();

        foreach (var valve in valves.Values)
        {
            valve.edges = valve.leadsToId.Select(l => new Edge { valve = valves[l] }).ToList();
            DebugLn(valve.ToString());
        }

        return valves;
    }

    static Dictionary<string, int> memob = new();

    static int CalculateBest(Valve startingValve, int minutesLeft, int players, ulong opened = 0)
    {
        memob.Clear();
        return BestInternal(startingValve, minutesLeft, startingValve, minutesLeft, players, opened);
    }

    static int Best(Valve startingValve, int minutesLeft, int players, ulong opened = 0)
    {
        return BestInternal(startingValve, minutesLeft, startingValve, minutesLeft, players, opened);
    }

    static int BestInternal(Valve startingValve, int playMinutes, Valve valve, int minutesLeft, int players, ulong opened)
    {
        string key = players + "/" + valve.mask + "/" + opened + "/" + minutesLeft;

        if (minutesLeft <= 1)
        {
            if (players > 1)
            {
                return Best(startingValve, playMinutes, --players, opened);
            }
            else
            {
                return 0;
            }
        }

        // Have we been here before, we know the answer if we have
        if (memob.ContainsKey(key))
        {
            return memob[key];
        }

        var best = 0;
        if ((opened & valve.mask) == 0)
        {
            int production = valve.rate * (minutesLeft - 1);
            var currentOpened = opened | valve.mask;
            foreach (var edge in valve.edges)
            {
                if (production != 0)
                {
                    best = Math.Max(best, production + BestInternal(startingValve, playMinutes, edge.valve, minutesLeft - 2, players, currentOpened));
                }
                best = Math.Max(best, BestInternal(startingValve, playMinutes, edge.valve, minutesLeft - 1, players, opened));
            }
        }
        else
        {
            foreach (var edge in valve.edges)
            {
                best = Math.Max(best, BestInternal(startingValve, playMinutes, edge.valve, minutesLeft - 1, players, opened));
            }
        }

        memob[key] = best;
        return best;
    }
}

