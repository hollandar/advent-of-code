using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem20;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem20", RowRegex());
        RunPartB("Problem20", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    private void Test()
    {
        {
            ArrayBlock one = new ArrayBlock(new long[] { -1, 2, 3 });
            one.Move(one.IndexOf(-1), -1);
            Assert(Enumerable.SequenceEqual(one.Values, new long[] { 2, -1, 3 }));

            ArrayBlock two = new ArrayBlock(new long[] { -1, 2, 3 });
            two.Move(two.IndexOf(-1), 1);
            Assert(Enumerable.SequenceEqual(two.Values, new long[] { 2, -1, 3 }));

            ArrayBlock three = new ArrayBlock(new long[] { -1, 2, 3 });
            three.Move(three.IndexOf(-1), 4);
            Assert(Enumerable.SequenceEqual(three.Values, new long[] { 2, 3, -1 }));

            ArrayBlock four = new ArrayBlock(new long[] { -1, 2, 3 });
            four.Move(four.IndexOf(-1), -4);
            Assert(Enumerable.SequenceEqual(four.Values, new long[] { 2, 3, -1 }));

            ArrayBlock onea = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            onea.Move(onea.IndexOf(-1), -1);
            Assert(Enumerable.SequenceEqual(onea.Values, new long[] { 2, 3, -1, 4 }));

            ArrayBlock twoa = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            twoa.Move(twoa.IndexOf(-1), 1);
            Assert(Enumerable.SequenceEqual(twoa.Values, new long[] { 2, -1, 3, 4 }));

            ArrayBlock threea = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            threea.Move(threea.IndexOf(-1), 4);
            Assert(Enumerable.SequenceEqual(threea.Values, new long[] { 2, -1, 3, 4 }));

            ArrayBlock foura = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            foura.Move(foura.IndexOf(-1), -4);
            Assert(Enumerable.SequenceEqual(foura.Values, new long[] { 2, 3, -1, 4 }));
        }

        {
            ArrayBlock one = new ArrayBlock(new long[] { -1, 2, 3 });
            one.Move(one.IndexOf(2), -1);
            Assert(Enumerable.SequenceEqual(one.Values, new long[] { -1, 3, 2 }));

            ArrayBlock two = new ArrayBlock(new long[] { -1, 2, 3 });
            two.Move(two.IndexOf(2), 1);
            Assert(Enumerable.SequenceEqual(two.Values, new long[] { -1, 3, 2 }));

            ArrayBlock three = new ArrayBlock(new long[] { -1, 2, 3 });
            three.Move(three.IndexOf(2), 4);
            Assert(Enumerable.SequenceEqual(three.Values, new long[] { -1, 2, 3 }));

            ArrayBlock four = new ArrayBlock(new long[] { -1, 2, 3 });
            four.Move(four.IndexOf(2), -4);
            Assert(Enumerable.SequenceEqual(four.Values, new long[] { -1, 2, 3 }));

            ArrayBlock onea = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            onea.Move(onea.IndexOf(2), -1);
            Assert(Enumerable.SequenceEqual(onea.Values, new long[] { -1, 3, 4, 2 }));

            ArrayBlock twoa = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            twoa.Move(twoa.IndexOf(2), 1);
            Assert(Enumerable.SequenceEqual(twoa.Values, new long[] { -1, 3, 2, 4 }));

            ArrayBlock threea = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            threea.Move(threea.IndexOf(2), 4);
            Assert(Enumerable.SequenceEqual(threea.Values, new long[] { -1, 3, 2, 4 }));

            ArrayBlock foura = new ArrayBlock(new long[] { -1, 2, 3, 4 });
            foura.Move(foura.IndexOf(2), -4);
            Assert(Enumerable.SequenceEqual(foura.Values, new long[] { -1, 3, 4, 2 }));
        }
        {
            ArrayBlock block = new ArrayBlock(new long[] { 1, 2, 0, 3, 4 });
            Assert(block.NthAfter(5, 0) == 0);
            Assert(block.NthAfter(10, 0) == 0);
            Assert(block.NthAfter(15, 0) == 0);
            Assert(block.NthAfter(20, 0) == 0);
            Assert(block.NthAfter(0, 0) == 0);
            Assert(block.NthAfter(1, 0) == 3);
            Assert(block.NthAfter(2, 0) == 4);
            Assert(block.NthAfter(3, 0) == 1);
            Assert(block.NthAfter(4, 0) == 2);
            Assert(block.NthAfter(5, 0) == 0);
            Assert(block.NthAfter(6, 0) == 3);
            Assert(block.NthAfter(7, 0) == 4);
            Assert(block.NthAfter(8, 0) == 1);
            Assert(block.NthAfter(9, 0) == 2);
        }
    }

    class ArrayCell
    {
        public long value;
        public int ix;
    }
    class ArrayBlock
    {
        int size;
        ArrayCell[] cells;

        public ArrayBlock(IEnumerable<long> elements)
        {
            size = elements.Count();

            ArrayCell? cell = null;
            int ix = 0;
            cells = new ArrayCell[size];
            foreach (var element in elements)
            {
                cell = new ArrayCell { value = element, ix = ix };
                cells[ix++] = cell;
            }
        }

        public IEnumerable<long> Values
        {
            get
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    yield return cells[i].value;
                }
            }
        }

        public int IndexOf(int n)
        {
            int i = 0;
            do
            {
                if (cells[i].value == n) return i;
                i++;
            } while (i < size);

            return -1;
        }

        public (int, long?) IndexOfPosition(int ix)
        {
            int i = 0;
            do
            {
                if (cells[i].ix == ix) return (i, cells[i].value);
                i++;
            } while (i < size);

            return (-1, null);
        }

        public void Move(int ix, long by)
        {
            if (by == 0) return;

            var value = cells[ix];
            Array.Copy(cells, ix + 1, cells, ix, size - ix - 1);

            ix = (int)(ix + by % (size - 1));
            while (ix < 0) ix += (size - 1);
            while (ix > size - 1) ix -= (size - 1);
            if (ix == 0) ix = size - 1;
            Array.Copy(cells, ix, cells, ix + 1, size - ix - 1);
            cells[ix] = value;
        }

        public long NthAfter(int c, int n)
        {
            var diff = c % size;

            int i = 0;
            while (cells[i].value != n)
            {
                i++;
            }

            while (diff-- > 0)
            {
                i++;
                if (i == size) i = 0;
            }

            return cells[i].value;
        }


    }
}

