using AOCLib;

namespace AdventOfCode2022.Problem18;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
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

        return notConnected.ToString();
    }

}
