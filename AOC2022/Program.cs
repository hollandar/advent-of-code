using AOCLib;

var problems = new Dictionary<string, IProblemPart>()
{
    ["1"] = new AdventOfCode2022.Problem01.Problem(),
    ["2"] = new AdventOfCode2022.Problem02.Problem(),
    ["3"] = new AdventOfCode2022.Problem03.Problem(),
    ["4"] = new AdventOfCode2022.Problem04.Problem(),
    ["5"] = new AdventOfCode2022.Problem05.Problem(),
    ["6"] = new AdventOfCode2022.Problem06.Problem(),
    ["7"] = new AdventOfCode2022.Problem07.Problem(),
    ["8"] = new AdventOfCode2022.Problem08.Problem(),
    ["9"] = new AdventOfCode2022.Problem09.Problem(),
    ["10"] = new AdventOfCode2022.Problem10.Problem(),
    ["11"] = new AdventOfCode2022.Problem11.Problem(),
    ["12"] = new AdventOfCode2022.Problem12.Problem(),
    ["13"] = new AdventOfCode2022.Problem13.Problem(),
    ["14"] = new AdventOfCode2022.Problem14.Problem(),
    ["15"] = new AdventOfCode2022.Problem15.Problem(),
    ["16"] = new AdventOfCode2022.Problem16.Problem(),
    ["17"] = new AdventOfCode2022.Problem17.Problem(),
    ["18"] = new AdventOfCode2022.Problem18.Problem(),
    ["19"] = new AdventOfCode2022.Problem19.Problem(),
    ["20"] = new AdventOfCode2022.Problem20.Problem(),
    ["21"] = new AdventOfCode2022.Problem21.Problem(),
    ["22"] = new AdventOfCode2022.Problem22.Problem(),
    ["23"] = new AdventOfCode2022.Problem23.Problem(),
    ["24"] = new AdventOfCode2022.Problem24.Problem(),
    ["25"] = new AdventOfCode2022.Problem25.Problem(),
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

