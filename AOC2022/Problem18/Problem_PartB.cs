using AOCLib;

namespace AdventOfCode2022.Problem18;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        HashSet<uint> cubes = new();
        foreach (var data in datas)
        {
            var cubeString = data.Value;
            var cubeCoords = cubeString.Split(",").Select(uint.Parse).ToArray();

            uint cubeCoord = cubeCoords[0] | (cubeCoords[1] << 8) | (cubeCoords[2] << 16);
            if (cubes.Contains(cubeCoord)) throw new Exception("Cube already defined");
            cubes.Add(cubeCoord);

        }



        var notConnected = cubes.Count * 6;
        foreach (var cube in cubes)
        {
            Coord c = Coords(cube);
            DebugLn($"x={c.x}; y={c.y}; z={c.z}");

            if (cubes.Contains(Encode(Left(c)))) notConnected--;
            if (cubes.Contains(Encode(Right(c)))) notConnected--;
            if (cubes.Contains(Encode(Front(c)))) notConnected--;
            if (cubes.Contains(Encode(Back(c)))) notConnected--;
            if (cubes.Contains(Encode(Up(c)))) notConnected--;
            if (cubes.Contains(Encode(Down(c)))) notConnected--;
        }

        DebugLn(notConnected.ToString());

        DebugLn("Encased cubes");
        uint maxx = cubes.Select(Coords).Max(r => r.x);
        uint maxy = cubes.Select(Coords).Max(r => r.y);
        uint maxz = cubes.Select(Coords).Max(r => r.z);
        uint minx = cubes.Select(Coords).Min(r => r.x);
        uint miny = cubes.Select(Coords).Min(r => r.y);
        uint minz = cubes.Select(Coords).Min(r => r.z);

        Coord zero = new Coord { x = 0, y = 0, z = 0 };
        Coord max = new Coord { x = maxx, y = maxy, z = maxz };

        for (uint x = minx; x <= maxx; x++)
        {

            for (uint y = miny; y <= maxy; y++)
            {

                for (uint z = minz; z <= maxz; z++)
                {
                    var coord = new Coord { x = x, y = y, z = z };
                    if (cubes.Contains(Encode(coord))) continue;

                    // If we cant flood fill to 20000 open cubes, we must be encased
                    var encased = !Reaches(cubes, coord, 20000);

                    if (encased)
                    {
                        DebugLn($"x={coord.x}; y={coord.y}; z={coord.z}");
                        if (cubes.Contains(Encode(Left(coord)))) notConnected--;
                        if (cubes.Contains(Encode(Right(coord)))) notConnected--;
                        if (cubes.Contains(Encode(Front(coord)))) notConnected--;
                        if (cubes.Contains(Encode(Back(coord)))) notConnected--;
                        if (cubes.Contains(Encode(Up(coord)))) notConnected--;
                        if (cubes.Contains(Encode(Down(coord)))) notConnected--;
                    }
                }
            }
        }

        return notConnected.ToString();
    }

}
