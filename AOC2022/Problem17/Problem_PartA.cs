using AOCLib;
using System.Diagnostics;

namespace AdventOfCode2022.Problem17;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var instructionString = datas.Single().Value;
        var instructions = instructionString.ToCharArray();
        int instructionIndex = 0;
        var nextInstruction = () =>
        {
            char i = instructions[instructionIndex];
            instructionIndex++;
            if (instructionIndex == instructions.Length)
            {
                instructionIndex = 0;
            }
            return i;
        };

        SlidingWindow area = new SlidingWindow();

        var player1 = new byte[] { 0b00011110 };
        var player1Positions = new byte[][]
        {
                MovePlayerLeft(MovePlayerLeft(player1)),
                MovePlayerLeft(player1),
                player1,
                MovePlayerRight(player1)
        };
        var player2 = new byte[] { 0b00001000,
                                       0b00011100,
                                       0b00001000 };
        var player2Positions = new byte[][]
        {
                MovePlayerLeft(MovePlayerLeft(player2)),
                MovePlayerLeft(player2),
                player2,
                MovePlayerRight(player2),
                MovePlayerRight(MovePlayerRight(player2))
        };
        var player3 = new byte[] { 0b00011100,
                                       0b00000100,
                                       0b00000100 };
        var player3Positions = new byte[][]
        {
                MovePlayerLeft(MovePlayerLeft(player3)),
                MovePlayerLeft(player3),
                player3,
                MovePlayerRight(player3),
                MovePlayerRight(MovePlayerRight(player3))
        };
        var player4 = new byte[] { 0b00010000,
                                       0b00010000,
                                       0b00010000,
                                       0b00010000 };
        var player4Positions = new byte[][]
        {
                MovePlayerLeft(MovePlayerLeft(player4)),
                MovePlayerLeft(player4),
                player4,
                MovePlayerRight(player4),
                MovePlayerRight(MovePlayerRight(player4)),
                MovePlayerRight(MovePlayerRight(MovePlayerRight(player4))),
                MovePlayerRight(MovePlayerRight(MovePlayerRight(MovePlayerRight(player4)))),
        };
        var player5 = new byte[] { 0b00011000,
                                       0b00011000 };
        var player5Positions = new byte[][]
        {
                MovePlayerLeft(MovePlayerLeft(player5)),
                MovePlayerLeft(player5),
                player5,
                MovePlayerRight(player5),
                MovePlayerRight(MovePlayerRight(player5)),
                MovePlayerRight(MovePlayerRight(MovePlayerRight(player5))),
        };
        var players = new byte[][][] { player1Positions, player2Positions, player3Positions, player4Positions, player5Positions };

        int player = 0;
        int height = 0;
        long stop = Stopwatch.GetTimestamp();
        HashSet<ulong> tracker = new HashSet<ulong>();
        var bounds = 2022;
        for (long round = 0; round < bounds; round++)
        {
            long playerHeight = area.Count + 3;
            int playerPosition = 2;
            do
            {
                byte[] sprite = players[player][playerPosition];
                {
                    // Move Left / Right
                    var move = nextInstruction();
                    if (instructionIndex == 0)
                    {
                        PrintArea(area.GetValues().Take(40));
                        DebugLn("Count:" + area.Count);
                        DebugLn("Round:" + round);

                    }
                    var newPosition = -1;
                    if (move == '<')
                        newPosition = playerPosition > 0 ? playerPosition - 1 : -1;
                    if (move == '>')
                        newPosition = playerPosition < players[player].Length - 1 ?
                            playerPosition + 1 : -1;
                    if (newPosition != -1 && !Collision(area, playerHeight, players[player][newPosition]))
                    {
                        sprite = players[player][newPosition];
                        playerPosition = newPosition;
                    }
                }

                {
                    // Move down
                    if (playerHeight <= area.Count)
                    {
                        if (playerHeight == 0)
                        {
                            PushPlayerToArea(area, playerHeight, sprite);
                            break;
                        }
                        else if (Collision(area, playerHeight - 1, sprite))
                        {
                            PushPlayerToArea(area, playerHeight, sprite);
                            break;
                        }
                        else
                        {
                            playerHeight--;
                        }
                    }
                    else
                    {
                        playerHeight--;
                    }
                }


            } while (playerHeight >= 0);

            player++;
            if (player > players.Length - 1)
            {
                player = 0;
            }

        }

        return area.Count.ToString();
    }

    class SlidingWindow
    {
        byte[] data = new byte[1024];
        long height = 0;

        public long Count { get { return height; } }

        public byte this[long index]
        {
            get { return data[index % data.LongLength]; }
            set { data[index % data.LongLength] = value; }
        }

        public IEnumerable<byte> GetValues()
        {
            for (long i = height - 1; i >= Math.Max(height - 100, 0); i--)
            {
                yield return this[i];
            }
        }
        public void Replace(long index, byte value)
        {
            this[index] = value;
        }
        public void Add(byte value)
        {
            this[height] = value;
            height++;
        }
    }

    byte[] ClonePlayer(byte[] player)
    {
        var result = new byte[player.Length];
        Array.Copy(player, result, player.Length);

        return result;
    }

    byte[] MovePlayerLeft(byte[] player)
    {
        var result = ClonePlayer(player);
        var canMove = true;
        foreach (var row in result)
        {
            if ((row & 0b01000000) != 0)
            {
                canMove = false;
            }
        }
        if (canMove)
        {
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(result[i] << 1);
            }
        }

        return result;
    }

    byte[] MovePlayerRight(byte[] player)
    {
        var result = ClonePlayer(player);
        var canMove = true;
        foreach (var row in result)
        {
            if ((row & 0b00000001) != 0)
            {
                canMove = false;
            }
        }
        if (canMove)
        {
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(result[i] >> 1);
            }
        }

        return result;
    }

    void PushPlayerToArea(SlidingWindow area, long height, byte[] player)
    {
        if (height > area.Count)
        {
            for (int i = 0; i < player.Length; i++)
            {
                area.Add(player[i]);
            }
        }
        else
        {
            for (int i = 0; i < player.Length; i++)
            {
                if ((height + i) < area.Count)
                {
                    var areaCell = area[height + i];
                    area.Replace(height + i, (byte)(areaCell | player[i]));
                }
                else
                {
                    area.Add(player[i]);
                }
            }
        }
    }

    bool Collision(SlidingWindow area, long height, byte[] player)
    {
        if (height > area.Count) return false;

        for (int i = 0; i < player.Length; i++)
        {
            if ((height + i) < area.Count)
            {
                var areaCell = area[height + i];
                if ((areaCell & player[i]) != 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void PrintArea(IEnumerable<byte> area)
    {
        foreach (var b in area)
        {
            var cell = b;
            for (int i = 0; i < 7; i++)
            {
                if ((cell & 0b01000000) != 0)
                {
                    Debug("#");
                }
                else
                {
                    Debug(".");
                }
                cell = (byte)(cell << 1);
            }

            DebugLn();
        }
        DebugLn();
    }
}
