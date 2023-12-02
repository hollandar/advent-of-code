using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem05;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem05", RowRegex());
        RunPartB("Problem05", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();
    
    [GeneratedRegex(@"(?:\[(.)\]|\s(\s)\s(?:\s|$))")]
    protected partial Regex StackLayoutRegex();

    [GeneratedRegex(@"move (\d+) from (\d+) to (\d+)")]
    protected partial Regex MoveRegex();

    public struct Move
    {
        public int Count;
        public int To;
        public int From;
    }

    public struct LoadedStacks
    {
        public Stack<char>[] Stacks;
        public List<Move> Moves;
    }

    protected LoadedStacks Load(IEnumerable<InputRow> lines)
    {
        // Load stacks

        var stacks = new List<char>[99];
        foreach (var line in lines)
        {
            var match = StackLayoutRegex().Matches(line.Value);
            if (!match.Any(r => r.Success))
                break;

            for (var j = 0; j < match.Count; j++)
            {
                if (match[j].Success && !String.IsNullOrWhiteSpace(match[j].Groups[1].Value))
                {
                    if (stacks[j] == null)
                        stacks[j] = new List<char>();
                    stacks[j].Add(match[j].Groups[1].Value[0]);
                }
            }
        }

        Stack<char>[] stacksArray = new Stack<char>[stacks.Where(r => r != null && r.Any()).Count()];

        for (int i = 0; i < stacksArray.Length; i++)
        {
            stacksArray[i] = new Stack<char>();
            stacks[i].Reverse();
            foreach (var item in stacks[i])
                stacksArray[i].Push(item);
        }


        // Load moves

        List<Move> moves = new();
        foreach (var line in lines)
        {
            var moveMatch = MoveRegex().Match(line.Value);
            if (moveMatch.Success)
            {
                int count = int.Parse(moveMatch.Groups[1].Value);
                int from = int.Parse(moveMatch.Groups[2].Value);
                int to = int.Parse(moveMatch.Groups[3].Value);
                moves.Add(new Move { Count = count, From = from, To = to });
            }
        }

        return new LoadedStacks { Stacks = stacksArray, Moves = moves };
    }

    protected void DebugStack(Stack<char>[] stacksArray)
    {
        if (InDebug)
        {
            int stackNumber = 1;
            foreach (var stack in stacksArray)
            {
                Debug($"{stackNumber++}: ", 2);
                foreach (var item in stack)
                {
                    Debug($"{item} ");
                }
                DebugLn();
            }
            DebugLn("--");
        }
    }
}

