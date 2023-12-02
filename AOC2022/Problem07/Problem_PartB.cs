using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem07;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartB(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        const int totalSize = 70000000;
        const int freeSpace = 30000000;
        int totalSpace = FolderSize(data);
        int unusedSpace = totalSize - totalSpace;
        DebugLn($"Unused {unusedSpace}");

        int needToFree = freeSpace - unusedSpace;
        DebugLn($"Need to free {needToFree}");

        var foldersSizes = new List<File>();
        FolderFilter(data, size => true, folder =>
        {
            var size = FolderSize(folder);
            if (size > needToFree)
            {
                foldersSizes.Add(new File { Name = folder.Name, Size = size });
            }
        });

        foreach (var file in foldersSizes.OrderBy(r => r.Size))
        {
            DebugLn($"{file.Name} {file.Size}");

        }

        var f = foldersSizes.OrderBy(r => r.Size).First();
        var answer = f.Size;

        return answer.ToString();
    }

}
