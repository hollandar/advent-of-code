using AOCLib;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace AdventOfCode2023.Problem05;

public partial class Problem : ProblemPart<InputRow>
{
    

    protected override string PartA(IEnumerable<InputRow> datas)
    {
        int answer = 0;
        int state = 0;

        ulong[] seeds = Array.Empty<ulong>();
        var seedToSoil = new List<NumberRange>();
        var soilToFertilizer = new List<NumberRange>();
        var fertilizerToWater = new List<NumberRange>();
        var waterToLight = new List<NumberRange>();
        var lightToTemperature = new List<NumberRange>();
        var temperatureToHumidity = new List<NumberRange>();
        var humidityToLocation = new List<NumberRange>();
            
        foreach (var data in datas)
        {
            if (data.Value.StartsWith("seeds:") && state == 0)
            {
                seeds = NumbersFromString(data.Value).ToArray();
                continue;
            }

            var oldState = state;
            state = data.Value switch
            {
                string s when s.StartsWith("seed-to-soil") => 1,
                string s when s.StartsWith("soil-to-fertilizer") => 2,
                string s when s.StartsWith("fertilizer-to-water") => 3,
                string s when s.StartsWith("water-to-light") => 4,
                string s when s.StartsWith("light-to-temperature") => 5,
                string s when s.StartsWith("temperature-to-humidity") => 6,
                string s when s.StartsWith("humidity-to-location") => 7,
                string s when String.IsNullOrWhiteSpace(s) => 8,
                _ => oldState
            };

            if (state != oldState) continue;

            switch (state)
            {
                case 1:
                    {
                        var values = NumbersFromString(data.Value).ToArray();
                        seedToSoil.Add(NumberRange.FromArray(values));
                    }
                    break;
                case 2:
                    {
                        var values = NumbersFromString(data.Value).ToArray();
                        soilToFertilizer.Add(NumberRange.FromArray(values));
                    }
                    break;
                case 3:
                    {
                        var values = NumbersFromString(data.Value).ToArray();
                        fertilizerToWater.Add(NumberRange.FromArray(values));
                    }
                    break;
                case 4:
                    {
                        var values = NumbersFromString(data.Value).ToArray();
                        waterToLight.Add(NumberRange.FromArray(values));
                    }
                    break;
                case 5:
                    {
                        var values = NumbersFromString(data.Value).ToArray();
                        lightToTemperature.Add(NumberRange.FromArray(values));
                    }
                    break;
                case 6:
                    {
                        var values = NumbersFromString(data.Value).ToArray();
                        temperatureToHumidity.Add(NumberRange.FromArray(values));
                    }
                    break;
                case 7:
                    {
                        var values = NumbersFromString(data.Value).ToArray();
                        humidityToLocation.Add(NumberRange.FromArray(values));
                    }
                    break;
                case 8:
                    continue;
            }
        }

        Assert(seeds.Length > 0);
        List<ulong> results = new();
        foreach (var seed in seeds)
        {
            var soil = seedToSoil.Where(r => r.InRange(seed)).Select(r => (ulong?)r.Value(seed)).SingleOrDefault() ?? seed;
            var fertilizer = soilToFertilizer.Where(r => r.InRange(soil)).Select(r => (ulong?)r.Value(soil)).SingleOrDefault() ?? soil;
            var water = fertilizerToWater.Where(r => r.InRange(fertilizer)).Select(r => (ulong?)r.Value(fertilizer)).SingleOrDefault() ?? fertilizer;
            var light = waterToLight.Where(r => r.InRange(water)).Select(r => (ulong?)r.Value(water)).SingleOrDefault() ?? water;
            var temperature = lightToTemperature.Where(r => r.InRange(light)).Select(r => (ulong?)r.Value(light)).SingleOrDefault() ?? light;
            var humidity = temperatureToHumidity.Where(r => r.InRange(temperature)).Select(r => (ulong?)r.Value(temperature)).SingleOrDefault() ?? temperature;
            var location = humidityToLocation.Where(r => r.InRange(humidity)).Select(r => (ulong?)r.Value(humidity)).SingleOrDefault() ?? humidity;

            results.Add(location);
        }

        return results.Min().ToString();
    }

    IEnumerable<(ulong from, ulong to)> RangeGenerator(ulong[] values)
    {
        ulong from = values[0];
        ulong to = values[1];
        ulong count = values[2];

        for (ulong i = 0; i < count; i++)
        {
            yield return (from + i, to + i);
        }
    }

    [GeneratedRegex("(\\d+)")]
    protected partial Regex NumberRegex();

    public IEnumerable<ulong> NumbersFromString(string s)
    {
        var matches = NumberRegex().Matches(s);
        foreach (Match match in matches)
        {
            yield return ulong.Parse(match.Value);
        }
    }

}
