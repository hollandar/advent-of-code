using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem07;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        const int maxSize = 100000;
        int biggerThanSize = 0;
        FolderFilter(data, size => true, folder =>
        {
            var size = FolderSize(folder);
            DebugLn($"{folder.Name} {size}");

            biggerThanSize += size <= maxSize ? size : 0;

        });

        return biggerThanSize.ToString();
    }

    void FolderFilter(Folder folder, Func<int, bool> filter, Action<Folder> action)
    {
        var size = FolderSize(folder);
        if (filter(size))
        {
            action(folder);
        }

        foreach (var sub in folder.Folders)
        {
            FolderFilter(sub, filter, action);
        }
    }

    int FolderSize(Folder folder)
    {
        int folderSize = 0;
        foreach (var file in folder.Files)
        {
            folderSize += file.Size;
        }

        foreach (var sub in folder.Folders)
        {
            folderSize += FolderSize(sub);
        }

        return folderSize;
    }

}
