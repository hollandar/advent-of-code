using AOCLib;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem24;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem24", RowRegex());
        RunPartB("Problem24", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    const byte unavailableSlot = 0;
    const byte availableSlot = 1;
    struct Point
    {
        public int x;
        public int y;
    }

    enum MovingEnum { North, South, East, West };

    struct Blizzard
    {
        public MovingEnum moving;
        public int id;

        public Blizzard(MovingEnum moving)
        {
            this.moving = moving;
            this.id = 0;
        }
    }

    enum KindEnum { Rock, Open }
    struct Cell
    {
        public KindEnum kind;
        public List<Blizzard> blizzards;

        public Cell(KindEnum kind, IEnumerable<Blizzard> blizzards)
        {
            this.kind = kind;
            this.blizzards = new(blizzards);
        }
    }

    class Arena
    {
        public Cell[,] cells;
        public int width;
        public int height;

        public Arena(int width, int height)
        {
            cells = new Cell[height, width];
            for (int w = 0; w < height; w++)
            {
                for (int h = 0; h < width; h++)
                {
                    cells[w, h] = new Cell() { blizzards = new() };
                }
            }
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var cell = cells[y, x];
                    if (cell.kind == KindEnum.Rock)
                        stringBuilder.Append("#");
                    else if (cell.kind == KindEnum.Open && cell.blizzards.Count == 0)
                        stringBuilder.Append(".");
                    else if (cell.kind == KindEnum.Open && cell.blizzards.Count == 1)
                        stringBuilder.Append(cell.blizzards[0].moving switch { MovingEnum.North => "^", MovingEnum.East => ">", MovingEnum.West => "<", MovingEnum.South => "v" });
                    else
                        stringBuilder.Append(cell.blizzards.Count > 9 ? "*" : cell.blizzards.Count);
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }

    struct Instruction
    {
        public long elapsed;
        public int minute;
        public int x;
        public int y;
    }

    (long elapsed, int minutes) Solve(Point start, Point end, int minute, byte[,,] openSlots, int width, int height)
    {
        Queue<Instruction> steps = new Queue<Instruction>();
        steps.Enqueue(new Instruction { x = start.x, y = start.y, elapsed = 0, minute = minute });

        long bestElapsed = long.MaxValue;
        int bestElapsedMinutes = int.MaxValue;
        HashSet<long> visited = new HashSet<long>();
        while (steps.Count > 0)
        {
            var step = steps.Dequeue();
            var ix = ((long)step.minute << 32) | ((long)step.y << 16) | ((long)step.x);
            if (visited.Contains(ix))
            {
                continue;
            }
            visited.Add(ix);
            minute = step.minute < openSlots.GetLength(0) - 2 ? step.minute + 1 : 0;

            if (InDebug)
            {
                DebugLn($"Minute: {step.minute} Elapsed: {step.elapsed}");
                for (int y1 = 0; y1 < height; y1++)
                {
                    for (int x1 = 0; x1 < width; x1++)
                    {
                        if (x1 == step.x && y1 == step.y)
                        {
                            Debug("E");
                        }
                        else if (openSlots[Math.Max(step.minute, 0), y1, x1] == availableSlot)
                        {
                            Debug(".");
                        }
                        else
                        {
                            Debug("#");
                        }
                    }
                    DebugLn();
                }
            }

            if (step.elapsed > bestElapsed)
            {
                continue;
            }

            if (step.x == end.x && step.y == end.y)
            {
                bestElapsed = Math.Min(bestElapsed, step.elapsed);
                bestElapsedMinutes = step.minute;
            }


            { // south
                var nextX = step.x;
                var nextY = step.y + 1;
                if (nextY < height && openSlots[minute, nextY, nextX] == availableSlot)
                {
                    steps.Enqueue(new Instruction { x = nextX, y = nextY, elapsed = step.elapsed + 1, minute = minute });
                }
            }
            { // east
                var nextX = step.x + 1;
                var nextY = step.y;
                if (nextX < width && openSlots[minute, nextY, nextX] == availableSlot)
                {
                    steps.Enqueue(new Instruction { x = nextX, y = nextY, elapsed = step.elapsed + 1, minute = minute });
                }
            }
            { // west
                var nextX = step.x - 1;
                var nextY = step.y;
                if (nextX >= 0 && openSlots[minute, nextY, nextX] == availableSlot)
                {
                    steps.Enqueue(new Instruction { x = nextX, y = nextY, elapsed = step.elapsed + 1, minute = minute });
                }
            }
            { // north
                var nextX = step.x;
                var nextY = step.y - 1;
                if (nextY >= 0 && openSlots[minute, nextY, nextX] == availableSlot)
                {
                    steps.Enqueue(new Instruction { x = nextX, y = nextY, elapsed = step.elapsed + 1, minute = minute });
                }
            }
            { // wait
                if (openSlots[minute, step.y, step.x] == availableSlot)
                    steps.Enqueue(new Instruction { x = step.x, y = step.y, elapsed = step.elapsed + 1, minute = minute });
            }
        }

        return (bestElapsed, bestElapsedMinutes);
    }

    static int simulationId = 0;
    static int SimulateBlizzards(Arena arena)
    {
        simulationId++;
        for (int y = 0; y < arena.height; y++)
        {
            for (int x = 0; x < arena.width; x++)
            {

                var cell = arena.cells[y, x];
                if (cell.kind == KindEnum.Rock || cell.blizzards.Count == 0)
                {
                    continue;
                }

                for (int i = 0; i < cell.blizzards.Count; i++)
                {
                    var blizzard = cell.blizzards[i];
                    if (blizzard.id == simulationId)
                        continue;
                    blizzard.id = simulationId;
                    switch (blizzard.moving)
                    {
                        case MovingEnum.North:
                            {
                                var nextCell = arena.cells[y - 1, x];
                                if (nextCell.kind == KindEnum.Rock)
                                {
                                    nextCell = arena.cells[arena.height - 2, x];
                                }
                                cell.blizzards.RemoveAt(i--);
                                nextCell.blizzards.Add(blizzard);
                                break;
                            }
                        case MovingEnum.East:
                            {
                                var nextCell = arena.cells[y, x + 1];
                                if (nextCell.kind == KindEnum.Rock)
                                {
                                    nextCell = arena.cells[y, 1];
                                }
                                cell.blizzards.RemoveAt(i--);
                                nextCell.blizzards.Add(blizzard);
                                break;
                            }
                        case MovingEnum.West:
                            {
                                var nextCell = arena.cells[y, x - 1];
                                if (nextCell.kind == KindEnum.Rock)
                                {
                                    nextCell = arena.cells[y, arena.width - 2];
                                }
                                cell.blizzards.RemoveAt(i--);
                                nextCell.blizzards.Add(blizzard);
                                break;
                            }
                        case MovingEnum.South:
                            {
                                var nextCell = arena.cells[y + 1, x];
                                if (nextCell.kind == KindEnum.Rock)
                                {
                                    nextCell = arena.cells[1, x];
                                }
                                cell.blizzards.RemoveAt(i--);
                                nextCell.blizzards.Add(blizzard);
                                break;
                            }
                    }
                }
            }
        }

        return simulationId;
    }

    Arena Load(IEnumerable<InputRow> datas)
    {
        var grid = new List<string>();
        foreach (var data in datas)
        {
            var line = data.Value;
            grid.Add(line);
        }

        var width = grid[0].Length;
        var height = grid.Count;
        var arena = new Arena(width, height);
        int y = 0;
        foreach (var row in grid)
        {
            int x = 0;
            foreach (var c in row)
            {
                switch (c)
                {
                    case '#':
                        {
                            arena.cells[y, x] = new Cell(KindEnum.Rock, Array.Empty<Blizzard>());
                            break;
                        }

                    case '<':
                        {
                            arena.cells[y, x] = new Cell(KindEnum.Open, new Blizzard[] { new Blizzard(MovingEnum.West) });
                            break;
                        }

                    case '>':
                        {
                            arena.cells[y, x] = new Cell(KindEnum.Open, new Blizzard[] { new Blizzard(MovingEnum.East) });
                            break;
                        }
                    case '^':
                        {
                            arena.cells[y, x] = new Cell(KindEnum.Open, new Blizzard[] { new Blizzard(MovingEnum.North) });
                            break;
                        }
                    case 'v':
                        {
                            arena.cells[y, x] = new Cell(KindEnum.Open, new Blizzard[] { new Blizzard(MovingEnum.South) });
                            break;
                        }
                    case '.':
                        {
                            arena.cells[y, x] = new Cell(KindEnum.Open, Array.Empty<Blizzard>());
                            break;

                        }
                }

                x++;
            }
            y++;
        }

        return arena;
    }
}

