using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOCLib;

public class Deserializer
{
    private string[] lines;

    public Deserializer(string fn)
    {
        this.lines = File.ReadAllLines(fn);
    }

    public List<T> Deserialize<T>(Regex regex)
    {
        var result = new List<T>();
        foreach (var line in lines)
        {
            var match = regex.Match(line);
            if (!match.Success)
            {
                throw new Exception("Failed to match line: " + line);
            }
            if (match.Success)
            {
                var record = (T)Activator.CreateInstance<T>();

                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    if (!match.Groups.ContainsKey(property.Name))
                    {
                        throw new MissingFieldException($"Missing field {property.Name}");
                    }
                    var value = match.Groups[property.Name].Value;
                    property.SetValue(record, Convert.ChangeType(value, property.PropertyType));
                }
                result.Add(record);
            }
        }

        return result;
    }
}
