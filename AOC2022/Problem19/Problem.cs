using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem19;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem19", RowRegex());
        RunPartB("Problem19", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    enum RobotEnum
    {
        ore, clay, obsidian,
        geode
    }
    struct RobotCost
    {
        public int clay;
        public int ore;
        public int obsidian;
        public RobotEnum type;
    }

    struct Blueprint
    {
        public int id;
        public RobotCost ore;
        public RobotCost clay;
        public RobotCost obsidian;
        public RobotCost geode;

        public int max_ore;
        public int max_clay;
        public int max_obsidian;
    }

    private int Max(params int[] t)
    {
        int r = t[0];
        for (int i = 1; i < t.Length; i++)
        {
            if (r < t[i]) r = t[i];
        }

        return r;
    }

    class Inventory
    {
        public int oreRobots;
        public int clayRobots;
        public int obsidianRobots;
        public int geodeRobots;

        public int ores;
        public int clays;
        public int obsidians;
        public int geodes;

        public int minutesLeft;

        public override string ToString()
        {
            return oreRobots + "-" + clayRobots + "-" + obsidianRobots + "-" + geodeRobots + "-" + ores + "-" + clays + "-" + obsidians + "-" + geodes + ":" + minutesLeft;
        }

        public Inventory Clone()
        {
            var i = new Inventory
            {
                oreRobots = this.oreRobots,
                clayRobots = this.clayRobots,
                obsidianRobots = this.obsidianRobots,
                geodeRobots = this.geodeRobots,

                ores = this.ores,
                clays = this.clays,
                obsidians = this.obsidians,
                geodes = this.geodes,

                minutesLeft = this.minutesLeft,
            };

            return i;
        }

        public ulong Thumbprint()
        {
            if (this.ores > 31 ||
                this.obsidians > 31 ||
                this.clays > 31 ||
                //this.geodes > 31 ||
                this.oreRobots > 31 ||
                this.clayRobots > 31 ||
                this.obsidianRobots > 31 ||
                this.geodeRobots > 31)
                throw new InvalidOperationException();

            return (ulong)this.ores |
                (ulong)this.clays << 5 |
                (ulong)this.obsidians << 10 |
                //(ulong)this.geodes << 15 |
                (ulong)this.oreRobots << 20 |
                (ulong)this.clayRobots << 25 |
                (ulong)this.obsidianRobots << 30 |
                (ulong)this.geodeRobots << 35 |
                (ulong)this.minutesLeft << 40;
        }
    }

    int Solve(Blueprint blueprint, Inventory inventory)
    {
        Queue<Inventory> queue = new Queue<Inventory>();
        queue.Enqueue(inventory);
        int bestGeodes = 0;
        HashSet<ulong> seen = new();
        while (queue.TryDequeue(out var inventoryNow))
        {
            if (inventoryNow.minutesLeft > 0)
            {
                var key = inventoryNow.Thumbprint();
                if (seen.Contains(key)) continue;
                seen.Add(key);

                if (inventoryNow.ores >= blueprint.ore.ore && inventoryNow.oreRobots < blueprint.max_ore)
                {
                    var next = inventoryNow.Clone();
                    next.ores = Math.Min(31, next.ores + next.oreRobots);
                    next.clays = Math.Min(31, next.clays + next.clayRobots);
                    next.obsidians = Math.Min(31, next.obsidians + next.obsidianRobots);
                    next.geodes += next.geodeRobots;
                    next.oreRobots++;
                    next.ores -= blueprint.ore.ore;
                    next.minutesLeft--;
                    queue.Enqueue(next);
                }

                if (inventoryNow.ores >= blueprint.clay.ore && inventoryNow.clayRobots < blueprint.max_clay)
                {
                    var next = inventoryNow.Clone();
                    next.ores = Math.Min(31, next.ores + next.oreRobots);
                    next.clays = Math.Min(31, next.clays + next.clayRobots);
                    next.obsidians = Math.Min(31, next.obsidians + next.obsidianRobots);
                    next.geodes += next.geodeRobots;
                    next.clayRobots++;
                    next.ores -= blueprint.clay.ore;
                    next.minutesLeft--;
                    queue.Enqueue(next);
                }

                if (inventoryNow.ores >= blueprint.obsidian.ore && inventoryNow.clays >= blueprint.obsidian.clay && inventoryNow.obsidianRobots < blueprint.max_obsidian)
                {
                    var next = inventoryNow.Clone();
                    next.ores = Math.Min(31, next.ores + next.oreRobots);
                    next.clays = Math.Min(31, next.clays + next.clayRobots);
                    next.obsidians = Math.Min(31, next.obsidians + next.obsidianRobots);
                    next.geodes += next.geodeRobots;
                    next.obsidianRobots++;
                    next.ores -= blueprint.obsidian.ore;
                    next.clays -= blueprint.obsidian.clay;
                    next.minutesLeft--;
                    queue.Enqueue(next);
                }

                if (inventoryNow.ores >= blueprint.geode.ore && inventoryNow.obsidians >= blueprint.geode.obsidian)
                {
                    var next = inventoryNow.Clone();
                    next.ores = Math.Min(31, next.ores + next.oreRobots);
                    next.clays = Math.Min(31, next.clays + next.clayRobots);
                    next.obsidians = Math.Min(31, next.obsidians + next.obsidianRobots);
                    next.geodes += next.geodeRobots;
                    next.geodeRobots++;
                    next.ores -= blueprint.geode.ore;
                    next.obsidians -= blueprint.geode.obsidian;
                    next.minutesLeft--;
                    queue.Enqueue(next);
                }

                var last = inventoryNow.Clone();
                last.ores = Math.Min(31, last.ores + last.oreRobots);
                last.clays = Math.Min(31, last.clays + last.clayRobots);
                last.obsidians = Math.Min(31, last.obsidians + last.obsidianRobots);
                last.geodes += last.geodeRobots;
                last.minutesLeft--;
                queue.Enqueue(last);
            }
            else
            {
                bestGeodes = Math.Max(bestGeodes, inventoryNow.geodes);
            }
        }
        return bestGeodes;
    }
    List<Blueprint> Load(IEnumerable<InputRow> datas)
    {
        var lineMatch = new Regex(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");
        List<Blueprint> blueprints = new List<Blueprint>();
        foreach (var data in datas)
        {
            var line = data.Value;
            var match = lineMatch.Match(line);
            if (match.Success)
            {
                int id = int.Parse(match.Groups[1].Value);
                var oreRobot = new RobotCost { ore = int.Parse(match.Groups[2].Value), type = RobotEnum.ore };
                var clayRobot = new RobotCost { ore = int.Parse(match.Groups[3].Value), type = RobotEnum.clay };
                var obsidianRobot = new RobotCost
                {
                    ore = int.Parse(match.Groups[4].Value),
                    clay = int.Parse(match.Groups[5].Value),
                    type = RobotEnum.obsidian
                };
                var geodeRobot = new RobotCost
                {
                    ore = int.Parse(match.Groups[6].Value),
                    obsidian = int.Parse(match.Groups[7].Value),
                    type = RobotEnum.geode
                };
                var blueprint = new Blueprint
                {
                    id = id,
                    ore = oreRobot,
                    clay = clayRobot,
                    obsidian = obsidianRobot,
                    geode = geodeRobot,
                    max_ore = Max(oreRobot.ore, clayRobot.ore, obsidianRobot.ore, geodeRobot.ore),
                    max_clay = Max(oreRobot.clay, clayRobot.clay, obsidianRobot.clay, geodeRobot.clay),
                    max_obsidian = Max(oreRobot.obsidian, clayRobot.obsidian, obsidianRobot.obsidian, geodeRobot.obsidian)
                };

                blueprints.Add(blueprint);
            }

        }

        return blueprints;
    }
}

