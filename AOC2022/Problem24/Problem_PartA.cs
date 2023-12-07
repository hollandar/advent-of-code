using AOCLib;

namespace AdventOfCode2022.Problem24;

public partial class Problem : ProblemPart<InputRow>
{
    

    // Find all the open slots in each round that can be moved through
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var arena = Load(datas);
        simulationId = 0;
        int slots = (arena.width - 2) * (arena.height - 2);
        var openSlots = new byte[slots, arena.height, arena.width];
        for (int i = 0; i < slots; i++)
        {
            for (int y1 = 0; y1 < arena.height; y1++)
            {
                for (int x1 = 0; x1 < arena.width; x1++)
                {
                    if (arena.cells[y1, x1].kind == KindEnum.Open && arena.cells[y1, x1].blizzards.Count == 0)
                    {
                        openSlots[i, y1, x1] = availableSlot;
                    }
                    else
                    {
                        openSlots[i, y1, x1] = unavailableSlot;
                    }
                }
            }

            SimulateBlizzards(arena);
        }

        // Solve by moving through open slots in each round
        long best;
        (best, _) = Solve(new Point { x = 1, y = 0 }, new Point { x = arena.width - 2, y = arena.height - 1 }, 0, openSlots, arena.width, arena.height);
        return best.ToString();
    }

}
