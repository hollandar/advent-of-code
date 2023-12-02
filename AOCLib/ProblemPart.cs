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
    public ProblemPart()
    {
        
    }
    public abstract bool Complete { get; }

    protected abstract long PartA(IEnumerable<TInputRow> input);
    protected abstract long PartB(IEnumerable<TInputRow> input);
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



}
