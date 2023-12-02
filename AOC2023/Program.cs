using AOCLib;

var problems = new Dictionary<string, IProblemPart>()
{
    ["1"] = new AdventOfCode2023.Problem01.Problem(),
    ["2"] = new AdventOfCode2023.Problem02.Problem(),
    ["3"] = new AdventOfCode2023.Problem03.Problem(),
    ["4"] = new AdventOfCode2023.Problem04.Problem(),
    ["5"] = new AdventOfCode2023.Problem05.Problem(),
    ["6"] = new AdventOfCode2023.Problem06.Problem(),
    ["7"] = new AdventOfCode2023.Problem07.Problem(),
    ["8"] = new AdventOfCode2023.Problem08.Problem(),
    ["9"] = new AdventOfCode2023.Problem09.Problem(),
    ["10"] = new AdventOfCode2023.Problem10.Problem(),
    ["11"] = new AdventOfCode2023.Problem11.Problem(),
    ["12"] = new AdventOfCode2023.Problem12.Problem(),
    ["13"] = new AdventOfCode2023.Problem13.Problem(),
    ["14"] = new AdventOfCode2023.Problem14.Problem(),
    ["15"] = new AdventOfCode2023.Problem15.Problem(),
    ["16"] = new AdventOfCode2023.Problem16.Problem(),
    ["17"] = new AdventOfCode2023.Problem17.Problem(),
    ["18"] = new AdventOfCode2023.Problem18.Problem(),
    ["19"] = new AdventOfCode2023.Problem19.Problem(),
    ["20"] = new AdventOfCode2023.Problem20.Problem(),
    ["21"] = new AdventOfCode2023.Problem21.Problem(),
    ["22"] = new AdventOfCode2023.Problem22.Problem(),
    ["23"] = new AdventOfCode2023.Problem23.Problem(),
    ["24"] = new AdventOfCode2023.Problem24.Problem(),
    ["25"] = new AdventOfCode2023.Problem25.Problem(),
};

if (problems.Values.Any(r => !r.Complete))
{
    problems.Values.Where(r => !r.Complete).First().Run();
    return;
}

Console.WriteLine("Problem:");
var problem = Console.ReadLine();

if (problems.TryGetValue(problem!, out var func))
{

    func.Run();
}
else { throw new Exception($"Problem {problem} not found"); }

