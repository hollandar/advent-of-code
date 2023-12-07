using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOCLib
{
    public static partial class Extensions
    {
        [GeneratedRegex("\\d+")]
        private static partial Regex NumberRegex();

        public static T[] NumbersFromString<T>(this string s)
        {
            return NumberRegex().Matches(s).Select(m => (T)Convert.ChangeType(m.Value, typeof(T))).ToArray();
        }
    }
}
