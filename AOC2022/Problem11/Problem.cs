using AOCLib;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problem11;

public partial class Problem : ProblemPart<InputRow>
{
    public override bool Complete => true;

    public override void Run()
    {
        RunPartA("Problem11", RowRegex());
        RunPartB("Problem11", RowRegex());
    }

    [GeneratedRegex("(?<Value>.*)")]
    public static partial Regex RowRegex();

    public struct Item
    {
        int id;
        long concern;

        public Item(int id, long itemConcern) : this()
        {
            this.id = id;
            this.concern = itemConcern;
        }

        public int Id => id;
        public long Concern => concern;
        public void SetConcern(long concern)
        {
            this.concern = concern;
        }

        public override string ToString()
        {
            return $"{id}-{concern}";
        }
    }

    public enum OperandEnum { Old, New, Number }
    public class Operand
    {
        OperandEnum operand;
        int? value = 0;

        public Operand(string operandValue)
        {
            if (int.TryParse(operandValue, out var n))
            {
                this.operand = OperandEnum.Number;
                this.value = n;
            }
            if (operandValue == "old")
            {
                this.operand = OperandEnum.Old;
            }
            if (operandValue == "new")
            {
                this.operand = OperandEnum.New;
            }
        }

        public OperandEnum Kind => operand;
        public int? Value => value;

        public override string ToString()
        {
            return this.operand switch
            {
                OperandEnum.Number => this.value.ToString() ?? "N/a",
                _ => this.operand.ToString()
            };
        }
    }

    public enum OperationEnum { Add, Multiply }
    public class Operation
    {
        Operand left;
        Operand right;
        OperationEnum operation;

        public Operation(Operand left, Operand right, string op)
        {
            this.left = left;
            this.right = right;
            this.operation = op switch
            {
                "+" => OperationEnum.Add,
                "*" => OperationEnum.Multiply,
                _ => throw new Exception($"Operation {op} not known.")
            };
        }

        public long Apply(long old)
        {
            long a = 0;
            if (this.left.Kind == OperandEnum.Old)
            {
                a = old;
            }
            if (this.left.Kind == OperandEnum.Number)
            {
                a = this.left.Value.Value;
            }
            long b = 0;
            if (this.right.Kind == OperandEnum.Old)
            {
                b = old;
            }
            if (this.right.Kind == OperandEnum.Number)
            {
                b = this.right.Value.Value;
            }

            return this.operation switch
            {
                OperationEnum.Multiply => a * b,
                OperationEnum.Add => a + b,
                _ => throw new Exception("Unknown operation")
            };

        }

        public override string ToString()
        {
            return $"{left} {this.operation.ToString()} {right}";
        }
    }

    public abstract class Test
    {
        public abstract bool IsTrue(long n);
    }

    public class DivisibleByTest : Test
    {
        private readonly long by;

        public DivisibleByTest(long by)
        {
            this.by = by;
        }

        public long By => by;

        public override bool IsTrue(long input)
        {
            var result = input % by == 0;
            return result;
        }

        public override string ToString()
        {
            return $"divisible by {by}";
        }
    }

    public class ThrowAction
    {
        private readonly int to;

        public ThrowAction(int to)
        {
            this.to = to;
        }

        public int To => to;
    }

    public class Monkey
    {
        int id;
        List<Item> items = new List<Item>();
        Operand set;
        Operation op;
        Test test;
        ThrowAction whenTrue;
        ThrowAction whenFalse;
        int inspected = 0;

        public Monkey(int id)
        {
            this.id = id;
        }

        public List<Item> Items => items;
        public Operand Set => set;
        public Operation Op => op;
        public Test Test => test;
        public ThrowAction WhenTrue => whenTrue;
        public ThrowAction WhenFalse => whenFalse;
        public int Inspected => inspected;
        public int Id => id;

        public void SetItems(List<Item> items)
        {
            this.items = items;
        }

        public void SetOperand(Operand op)
        {
            this.set = op;
        }

        public void SetOperation(Operation op)
        {
            this.op = op;
        }

        public void SetTest(Test test)
        {
            this.test = test;
        }

        public void SetActionTrue(ThrowAction throwAction)
        {
            this.whenTrue = throwAction;
        }

        public void SetActionFalse(ThrowAction throwAction)
        {
            this.whenFalse = throwAction;
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void IncrementInspected()
        {
            inspected++;
        }
    }


    protected Dictionary<int, Monkey> Load(IEnumerable<InputRow> datas)
    {
        var monkeys = new Dictionary<int, Monkey>();

        var monkeyRegex = new Regex(@"^Monkey (\d+):$");
        var startingItemsRegex = new Regex(@"^\s\sStarting items: (?:(\d+)(?:,\s|$))+");
        var operationRegex = new Regex(@"^\s\sOperation: (new)\s=\s(?:(new|old|\d+)(?:\s(.)\s)(old|new|\d+))$");
        var testRegex = new Regex(@"^\s\sTest: (.*?)\s(\d+)$");
        var ifRegex = new Regex(@"^\s\s\s\sIf (.*?): throw to monkey (\d+)$");
        Monkey? monkey = null;
        int itemNumber = 0;
        foreach (var line in datas)
        {

            var monkeyMatch = monkeyRegex.Match(line.Value);
            if (monkeyMatch.Success)
            {
                var id = int.Parse(monkeyMatch.Groups[1].Value);
                monkey = new Monkey(id);
                monkeys[id] = monkey;
                continue;
            }

            var startingItemsMatch = startingItemsRegex.Match(line.Value);
            if (startingItemsMatch.Success)
            {
                List<Item> items = new();
                var group = startingItemsMatch.Groups[1];
                for (int i = 0; i < group.Captures.Count; i++)
                {
                    var itemConcern = int.Parse(group.Captures[i].Value);
                    items.Add(new Item(itemNumber++, itemConcern));
                }

                System.Diagnostics.Debug.Assert(monkey != null);
                monkey.SetItems(items);
                continue;
            }

            var operationMatch = operationRegex.Match(line.Value);
            if (operationMatch.Success)
            {
                if (operationMatch.Groups.Count != 5)
                    throw new Exception($"Operation count mismatch in {line}");
                System.Diagnostics.Debug.Assert(monkey != null);
                monkey.SetOperand(new Operand(operationMatch.Groups[1].Value));
                Operation op = new Operation(
                    new Operand(operationMatch.Groups[2].Value),
                    new Operand(operationMatch.Groups[4].Value),
                    operationMatch.Groups[3].Value
                    );
                monkey.SetOperation(op);


                continue;
            }

            var testMatch = testRegex.Match(line.Value);
            if (testMatch.Success)
            {
                var opString = testMatch.Groups[1].Value;
                var number = int.Parse(testMatch.Groups[2].Value);
                monkey.SetTest(opString switch
                {
                    "divisible by" => new DivisibleByTest(number),
                    _ => throw new Exception($"Unknown test {line}")
                });
                continue;
            }

            var ifMatch = ifRegex.Match(line.Value);
            if (ifMatch.Success)
            {
                var boolOp = bool.Parse(ifMatch.Groups[1].Value);
                var number = int.Parse(ifMatch.Groups[2].Value);

                if (boolOp)
                {
                    monkey.SetActionTrue(new ThrowAction(number));
                }
                else
                {
                    monkey.SetActionFalse(new ThrowAction(number));
                }
                continue;
            }

            if (!String.IsNullOrEmpty(line.Value))
            {
                throw new Exception($"Unexpected {line.Value}");
            }
        }

        return monkeys;
    }

    protected void MonkeyBusiness(Dictionary<int, Monkey> monkeys, Func<long, long> adjust)
    {
        foreach (var monkeyId in monkeys.Keys.OrderBy(r => r))
        {
            var currentMonkey = monkeys[monkeyId];
            foreach (var item in currentMonkey.Items.ToList())
            {
                var concern = item.Concern;
                var newConcern = currentMonkey.Op.Apply(concern);

                newConcern = adjust(newConcern);

                if (currentMonkey.Set.Kind != OperandEnum.New)
                    throw new Exception("Unexpected operand target");

                ThrowAction action;
                var result = currentMonkey.Test.IsTrue(newConcern);
                if (result)
                {
                    action = currentMonkey.WhenTrue;
                }
                else
                {
                    action = currentMonkey.WhenFalse;
                }

                var toMonkey = monkeys[action.To];
                currentMonkey.RemoveItem(item);

                Assert(newConcern >= 0);

                item.SetConcern(newConcern);

                toMonkey.AddItem(item);

                currentMonkey.IncrementInspected();
            }

        }
    }

}

