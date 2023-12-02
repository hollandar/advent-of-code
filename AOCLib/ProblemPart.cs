using Humanizer;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AOCLib;

public interface IProblemPart
{
    void Run();
    public bool Complete { get; }
}

public abstract class ProblemPart<TInputRow>: IProblemPart
{
    bool debug = false;

    public ProblemPart()
    {
        
    }
    
    public abstract bool Complete { get; }
    protected bool InDebug => debug;

    public void SetDebug(bool debug = true)
    {
        this.debug = debug;
    }

    protected abstract string PartA(IEnumerable<TInputRow> input);
    protected abstract string PartB(IEnumerable<TInputRow> input);
    public abstract void Run();

    protected void RunPartA(string folder, Regex regex)
    {
        var start = Stopwatch.GetTimestamp();
        Console.WriteLine("{0} {1}: {2} took {3}",
            folder,
            "Sample A",
            PartA(new Deserializer($"{folder}/sample.a.txt").Deserialize<TInputRow>(regex)),
            Stopwatch.GetElapsedTime(start).Humanize()
        );
        start = Stopwatch.GetTimestamp();
        Console.WriteLine("{0} {1}: {2} took {3}",
            folder,
            "Input",
            PartA(new Deserializer($"{folder}/input.txt").Deserialize<TInputRow>(regex)),
            Stopwatch.GetElapsedTime(start).Humanize()
        );
    }
    
    protected void RunPartB(string folder, Regex regex)
    {
        var start = Stopwatch.GetTimestamp();
        Console.WriteLine("{0} {1}: {2} took {3}",
            folder,
            "Sample B",
            PartB(new Deserializer($"{folder}/sample.b.txt").Deserialize<TInputRow>(regex)),
            Stopwatch.GetElapsedTime(start).Humanize()
        );
        start = Stopwatch.GetTimestamp();
        Console.WriteLine("{0} {1}: {2} took {3}",
            folder,
            "Input",
            PartB(new Deserializer($"{folder}/input.txt").Deserialize<TInputRow>(regex)),
            Stopwatch.GetElapsedTime(start).Humanize()
        );

    }


    protected void DebugLn(string s = "", int indent = 2, ConsoleColor color = ConsoleColor.White)
    {
        if (debug) PrintLn(s, indent, color);
    }

    protected void PrintLn(string s = "", int indent = 2, ConsoleColor color = ConsoleColor.White)
    {
        for (int i = 0; i < indent; i++)
        {
            Console.Write("  ");
        }

        Console.ForegroundColor = color;
        Console.WriteLine(s);
    }


    protected void Debug(string s = "", int indent = 0, ConsoleColor color = ConsoleColor.White)
    {
        if (debug) Print(s, indent, color);
    }

    protected void Print(string s = "", int indent = 0, ConsoleColor color = ConsoleColor.White)
    {
        for (int i = 0; i < indent; i++)
        {
            Console.Write("  ");
        }

        Console.ForegroundColor = color;
        Console.Write(s);
    }

    public void Assert(bool condition, string message = "")
    {
        System.Diagnostics.Debug.Assert(condition, message);
    }
}
