using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem08;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        int highestScore = 0;
        int highestX = 0, highestY = 0;
        for (int y = 0; y < data.Height; y++)
        {
            Debug(indent: 2);
            for (int x = 0; x < data.Width; x++)
            {
                (var visible, var treeScore) = TreeScore(data, x, y);
                if (treeScore > highestScore)
                {
                    highestScore = treeScore;
                    highestX = x; highestY = y;
                }
            }
            DebugLn(indent: 0);
        }

        DebugLn($"Highest Score = {highestScore} at ({highestX}, {highestY})");

        return highestScore.ToString();
    }

}
