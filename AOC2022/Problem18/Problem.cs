using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem18;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem18", RowRegex());
        RunPartB("Problem18", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    struct Coord { public uint x; public uint y; public uint z; }

    static Coord Left(Coord c) => new Coord { x = c.x - 1, y = c.y, z = c.z };
    static Coord Right(Coord c) => new Coord { x = c.x + 1, y = c.y, z = c.z };
    static Coord Up(Coord c) => new Coord { x = c.x, y = c.y - 1, z = c.z };
    static Coord Down(Coord c) => new Coord { x = c.x, y = c.y + 1, z = c.z };
    static Coord Front(Coord c) => new Coord { x = c.x, y = c.y, z = c.z + 1 };
    static Coord Back(Coord c) => new Coord { x = c.x, y = c.y, z = c.z - 1 };
    static Coord Coords(uint coord)
    {
        return new Coord { x = (coord & 0xff), y = (coord >> 8 & 0xff), z = (coord >> 16 & 0xff) };
    }

    static uint Encode(Coord c)
    {
        return c.x | (c.y << 8) | (c.z << 16);
    }

    static bool Reaches(HashSet<uint> cubes, Coord a, int cycles = 10000)
    {
        HashSet<uint> visited = new();
        Queue<uint> nodesToVisit = new();
        Coord current = a;
        while (cycles > 0)
        {
            if (!cubes.Contains(Encode(current)))
            {
                nodesToVisit.Enqueue(Encode(Left(current)));
                nodesToVisit.Enqueue(Encode(Right(current)));
                nodesToVisit.Enqueue(Encode(Up(current)));
                nodesToVisit.Enqueue(Encode(Down(current)));
                nodesToVisit.Enqueue(Encode(Front(current)));
                nodesToVisit.Enqueue(Encode(Back(current)));
            }

            if (nodesToVisit.Count == 0)
                return false;

            var nextNode = nodesToVisit.Dequeue();
            while (nodesToVisit.Count > 0 && visited.Contains(nextNode))
                nextNode = nodesToVisit.Dequeue();

            visited.Add(nextNode);
            current = Coords(nextNode);
            cycles--;
        }

        return true;


    }
}

