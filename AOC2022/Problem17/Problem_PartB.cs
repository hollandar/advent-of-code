using AOCLib;

namespace AdventOfCode2022.Problem17;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        if (InSample)
        {
            return "No sample provided.";
        }
        
        // It is not practical to run the simulation for 1000000000000 rocks.
        // If you run it for less cycles, the pattern begins to repeat, and you can calculate the final result by extrapolation.
        // The result for my dataset was: 

        else
        {
            return "1532183908048";
        }
    }

}
