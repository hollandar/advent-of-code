using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem07;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem07", RowRegex());
        RunPartB("Problem07", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    public class File
    {
        public string Name = string.Empty;
        public int Size;
    }

    public class Folder
    {
        public string Name = string.Empty;
        public List<Folder> Folders = new List<Folder>();
        public List<File> Files = new List<File>();
        public Folder? ParentFolder = null;
    }

    protected Folder Load(IEnumerable<InputRow> datas)
    {
        var root = new Folder { Name = "/" };

        Folder currentFolder = root;
        bool listing = false;
        foreach (var line in datas)
        {
            if (line.Value == "$ cd /")
            {
                currentFolder = root;
                listing = false;
                continue;
            }

            var cdRegex = new Regex(@"^\$ cd ([^\s]+)$");
            var cdMatch = cdRegex.Match(line.Value);
            if (cdMatch.Success)
            {
                var name = cdMatch.Groups[1].Value;
                if (name == "..")
                {
                    Assert(currentFolder.ParentFolder is not null);
                    currentFolder = currentFolder.ParentFolder!;
                }
                else
                {
                    currentFolder = currentFolder.Folders.Where(r => r.Name == name).Single();
                }
                listing = false;
                continue;
            }

            if (line.Value == "$ ls")
            {
                listing = true;
                continue;
            }

            var dirRegex = new Regex(@"^(dir|\d+) (.*)$");
            var match = dirRegex.Match(line.Value);
            if (listing && match.Success)
            {

                if (match.Groups[1].Value == "dir")
                {
                    var name = match.Groups[2].Value;
                    var folder = new Folder { Name = name, ParentFolder = currentFolder };
                    currentFolder.Folders.Add(folder);
                }
                else
                {
                    var size = int.Parse(match.Groups[1].Value);
                    var name = match.Groups[2].Value;
                    var file = new File { Name = name, Size = size };
                    currentFolder.Files.Add(file);
                }
            }
        }

        return root;
    }
}

