using AOCLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2022.Problem08;

public partial class Problem : ProblemPart<InputRow>
{
    protected override string PartA(IEnumerable<InputRow> datas)
    {
        var data = Load(datas);

        int visibleTrees = 0;
        for (int y = 0; y < data.Height; y++)
        {
            Debug(indent: 2);
            for (int x = 0; x < data.Width; x++)
            {
                (var visible, var treeScore) = TreeScore(data, x, y);
                if (visible) visibleTrees++;
            }
            DebugLn(indent: 0);
        }

        return visibleTrees.ToString();
    }

    int GetCell(TreeMap map, int x, int y)
    {
        var cell = (y * map.Width) + x;
        return map.Trees[cell];
    }

    (bool visible, int score) TreeScore(TreeMap map, int x, int y)
    {
        bool visible = false;
        var treeHeight = GetCell(map, x, y);
        bool vu = true, vd = true, vl = true, vr = true;
        bool vus = true, vds = true, vls = true, vrs = true;
        int vsu = 0, vsd = 0, vsl = 0, vsr = 0;
        for (int xl = x - 1; xl >= 0; xl--)
        {
            var thisTreeHeight = GetCell(map, xl, y);
            if (thisTreeHeight >= treeHeight) vl = false;
            if (vls)
            {
                if (treeHeight <= thisTreeHeight)
                {
                    vls = false;
                }
                vsl++;
            }
        }

        for (int xr = x + 1; xr < map.Width; xr++)
        {
            var thisTreeHeight = GetCell(map, xr, y);
            if (thisTreeHeight >= treeHeight) vr = false;
            if (vrs)
            {
                if (treeHeight <= thisTreeHeight)
                {
                    vrs = false;
                }
                vsr++;
            }
        }

        for (int yu = y - 1; yu >= 0; yu--)
        {
            var thisTreeHeight = GetCell(map, x, yu);
            if (thisTreeHeight >= treeHeight) vu = false;
            if (vus)
            {
                if (treeHeight <= thisTreeHeight)
                {
                    vus = false;
                }
                vsu++;
            }
        }

        for (int yd = y + 1; yd < map.Height; yd++)
        {
            var thisTreeHeight = GetCell(map, x, yd);
            if (thisTreeHeight >= treeHeight) vd = false;
            if (vds)
            {
                if (treeHeight <= GetCell(map, x, yd))
                {
                    vds = false;
                }
                vsd++;
            }
        }

        if (vl || vr || vu || vd)
        {
            Debug("1");
            visible = true;
        }
        else
        {
            Debug(".");
        }

        var treeScore = vsu * vsd * vsl * vsr;

        return (visible, treeScore);
    }
}
