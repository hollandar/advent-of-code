using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem13;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => false;

    public override void Run()
    {
        RunPartA("Problem13", RowRegex());
        RunPartB("Problem13", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    public class ListOrInt : IComparable
    {
        public int? n = null;
        public List<ListOrInt>? list = null;
        public ListOrInt(int n)
        {
            this.n = n;
        }
        public ListOrInt(List<ListOrInt> l)
        {
            this.list = l;
        }

        public int CompareTo(object? obj)
        {
            var t = obj as ListOrInt;
            if (t == null) throw new Exception();
            return (int)Compare(this, t);
        }

        public override string ToString()
        {
            if (n.HasValue) return n.Value.ToString();
            if (list != null)
            {
                return $"[{String.Join(",", list)}]";
            }
            return "X";
        }
    }


    protected List<ListOrInt> Load(IEnumerable<InputRow> datas)
    {
        List<ListOrInt> list = new();
        foreach (var line in datas)
        {
            if (!String.IsNullOrWhiteSpace(line.Value))
            {
                var list1 = ParseList(line.Value);
                list.Add(list1.Item2);
            }
        }

        return list;

    }

    static (int, ListOrInt) ParseList(string line)
    {
        if (!line.StartsWith('[') && !line.EndsWith(']'))
        {
            line = $"[{line}]";
        }

        List<ListOrInt> list = new();
        int num = -1;
        for (int i = 0; i < line.Length; i++)
        {
            if (i == 0 && line[i] == '[')
            {
                continue;
            }
            if (line[i] == '[')
            {
                (var length, var innerList) = ParseList(line.Substring(i));
                list.Add(innerList);
                i += length;
                continue;
            }
            if (line[i] >= '0' && line[i] <= '9')
            {
                if (num == -1) num = 0;
                num = (num * 10) + (line[i] - '0');
                continue;
            }
            if (line[i] == ',' || line[i] == ']')
            {
                if (num != -1) list.Add(new ListOrInt(num));
                num = -1;
            }
            if (line[i] == ']')
            {
                return (i, new ListOrInt(list));
            }
        }
        throw new Exception("Array not terminated");
    }

    static List<int> Unwrap(List<ListOrInt> list)
    {
        var innerList = new List<int>();
        foreach (var val in list)
        {
            if (val.n != null)
            {
                innerList.Add(val.n.Value);
            }
            else if (val.list != null)
            {
                var wrappedList = Unwrap(val.list);
                innerList.AddRange(wrappedList);
            }
        }

        return innerList;
    }

    enum CompareEnum { Valid = -1, Invalid = 1, MoveOn = 0 }
    static CompareEnum Compare(ListOrInt left, ListOrInt right)
    {
        if (left.n != null && right.n != null)
        {
            if (left.n < right.n)
            {
                return CompareEnum.Valid;
            }
            if (left.n > right.n)
            {
                return CompareEnum.Invalid;
            }
            if (left.n == right.n)
            {
                return CompareEnum.MoveOn;
            }
        }

        if (left.n != null && right.list != null)
        {
            var result = Compare(new ListOrInt(new List<ListOrInt> { left }), right);
            if (result != CompareEnum.MoveOn) return result;
        }

        if (left.list != null && right.n != null)
        {
            var result = Compare(left, new ListOrInt(new List<ListOrInt> { right }));
            if (result != CompareEnum.MoveOn) return result;
        }

        if (left.list != null && right.list != null)
        {
            for (int i = 0; i < Math.Max(left.list.Count, right.list.Count); i++)
            {
                if (i >= left.list.Count)
                {
                    return CompareEnum.Valid;
                }

                if (i >= right.list.Count)
                {
                    return CompareEnum.Invalid;
                }

                var result = Compare(left.list[i], right.list[i]);
                if (result != CompareEnum.MoveOn) return result;
            }
        }

        return CompareEnum.MoveOn;
    }
}

