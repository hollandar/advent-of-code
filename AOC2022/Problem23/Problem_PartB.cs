using AOCLib;
using AOCLib.Primitives;

namespace AdventOfCode2022.Problem23;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        List<Elf> elves = new List<Elf>();
        long y = 0;
        long x = 0;
        Bounds mapSize = new Bounds();

        foreach (var data in datas)
        {
            var line = data.Value;
            x = 0;
            while (x < line.Length)
            {
                if (line[(int)x] == '#')
                {
                    var elf = new Elf
                    {
                        position = new Point(x, y)
                    };
                    elves.Add(elf);
                }
                x++;
            }
            y++;
        }

        mapSize = new Bounds(x - 1, y - 1);


        var area = new Cell[mapSize.Height * 3, mapSize.Width * 3];
        Point zero = new Point(mapSize.Width, mapSize.Height);
        foreach (var elf in elves)
        {
            area[elf.position.Y + zero.Y, elf.position.X + zero.X].elves = 1;
        }

        PrintArea(mapSize, zero, area);

        bool finished = false;
        int round = 0;
        while (!finished)
        {
            finished = true;
            // round 1a
            foreach (var elf in elves)
            {
                elf.valid_proposal = false;

                if (
                    area[elf.position.Y + 1 + zero.Y, elf.position.X + zero.X].elves +
                    area[elf.position.Y + 1 + zero.Y, elf.position.X + 1 + zero.X].elves +
                    area[elf.position.Y + zero.Y, elf.position.X + 1 + zero.X].elves +
                    area[elf.position.Y - 1 + zero.Y, elf.position.X + 1 + zero.X].elves +
                    area[elf.position.Y - 1 + zero.Y, elf.position.X + zero.X].elves +
                    area[elf.position.Y - 1 + zero.Y, elf.position.X - 1 + zero.X].elves +
                    area[elf.position.Y + zero.Y, elf.position.X - 1 + zero.X].elves +
                    area[elf.position.Y + 1 + zero.Y, elf.position.X - 1 + zero.X].elves
                    == 0)
                    continue;

                finished = false;
                for (int c = 0; c < 4; c++)
                {
                    switch ((c + round) % 4)
                    {
                        case 0: // N,NE,NW
                            {
                                if (area[elf.position.Y - 1 + zero.Y, elf.position.X + zero.X].elves +
                                area[elf.position.Y - 1 + zero.Y, elf.position.X + 1 + zero.X].elves +
                                area[elf.position.Y - 1 + zero.Y, elf.position.X - 1 + zero.X].elves == 0)
                                {
                                    elf.proposal = new Point(elf.position.X, elf.position.Y - 1);
                                    area[elf.proposal.Y + zero.Y, elf.proposal.X + zero.X].proposals += 1;
                                    elf.valid_proposal = true;
                                }
                                break;
                            }
                        case 1: // S,SE,SW
                            {
                                if (area[elf.position.Y + 1 + zero.Y, elf.position.X + zero.X].elves +
                                area[elf.position.Y + 1 + zero.Y, elf.position.X + 1 + zero.X].elves +
                                area[elf.position.Y + 1 + zero.Y, elf.position.X - 1 + zero.X].elves == 0)
                                {
                                    elf.proposal = new Point(elf.position.X, elf.position.Y + 1);
                                    area[elf.proposal.Y + zero.Y, elf.proposal.X + zero.X].proposals += 1;
                                    elf.valid_proposal = true;
                                }
                                break;
                            }
                        case 2: // W,NW,SW
                            {
                                if (area[elf.position.Y + zero.Y, elf.position.X - 1 + zero.X].elves +
                                area[elf.position.Y + 1 + zero.Y, elf.position.X - 1 + zero.X].elves +
                                area[elf.position.Y - 1 + zero.Y, elf.position.X - 1 + zero.X].elves == 0)
                                {
                                    elf.proposal = new Point(elf.position.X - 1, elf.position.Y);
                                    area[elf.proposal.Y + zero.Y, elf.proposal.X + zero.X].proposals += 1;
                                    elf.valid_proposal = true;
                                }
                                break;
                            }
                        case 3: // E,NE, SE
                            {
                                if (area[elf.position.Y + zero.Y, elf.position.X + 1 + zero.X].elves +
                                area[elf.position.Y + 1 + zero.Y, elf.position.X + 1 + zero.X].elves +
                                area[elf.position.Y - 1 + zero.Y, elf.position.X + 1 + zero.X].elves == 0)
                                {
                                    elf.proposal = new Point(elf.position.X + 1, elf.position.Y);
                                    area[elf.proposal.Y + zero.Y, elf.proposal.X + zero.X].proposals += 1;
                                    elf.valid_proposal = true;
                                }
                                break;
                            }
                    }

                    if (elf.valid_proposal) break;
                }
            }

            //round 1b
            var moved = false;
            foreach (var elf in elves)
            {
                if (elf.valid_proposal)
                {
                    if (area[elf.proposal.Y + zero.Y, elf.proposal.X + zero.X].proposals == 1)
                    {
                        area[elf.position.Y + zero.Y, elf.position.X + zero.X].elves = 0;
                        elf.position = elf.proposal;
                        area[elf.position.Y + zero.Y, elf.position.X + zero.X].elves = 1;

                        if (elf.position.X < mapSize.TopLeft.X) mapSize = mapSize.Clone(topLeft: new Point(elf.position.X, mapSize.TopLeft.Y));
                        if (elf.position.Y < mapSize.TopLeft.Y) mapSize = mapSize.Clone(topLeft: new Point(mapSize.TopLeft.X, elf.position.Y));
                        if (elf.position.X > mapSize.BottomRight.X) mapSize = mapSize.Clone(bottomRight: new Point(elf.position.X, mapSize.BottomRight.Y));
                        if (elf.position.Y > mapSize.BottomRight.Y) mapSize = mapSize.Clone(bottomRight: new Point(mapSize.BottomRight.X, elf.position.Y));

                        moved = true;
                    }

                    area[elf.proposal.Y + zero.Y, elf.proposal.X + zero.X].proposals = 0;
                }
            }


            DebugLn("Round" + ++round);
            PrintArea(mapSize, zero, area);

            if (!moved)
                return round.ToString();
        }

        return "OOPS";
    }

}
