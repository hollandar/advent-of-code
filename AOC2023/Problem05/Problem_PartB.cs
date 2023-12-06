using AOCLib;
using Humanizer.Localisation.TimeToClockNotation;

namespace AdventOfCode2023.Problem05;

public partial class Problem : ProblemPart<InputRow>
{
    record ItemRange(ulong From, ulong Count);

    protected override string PartB(IEnumerable<InputRow> datas)
    {
        SetDebug();

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

        
        for (int i = 0; i < seeds.Length; i += 2)
        {
            var from = seeds[i];
            var count = seeds[i + 1];

            // Brute force does not work for such a large list of numbers, need to do range construction instead
            var soilRanges = RangeHelper(seedToSoil, new ItemRange[] { new ItemRange(from,count) });
            var fertilizerRanges = RangeHelper(soilToFertilizer, soilRanges);
            var waterRanges = RangeHelper(fertilizerToWater, fertilizerRanges);
            var lightRanges = RangeHelper(waterToLight, waterRanges);
            var temperatureRanges = RangeHelper(lightToTemperature, lightRanges);
            var humidityRanges = RangeHelper(temperatureToHumidity, temperatureRanges);
            var locationRanges = RangeHelper(humidityToLocation, humidityRanges);

            results.Add(locationRanges.Min(r => r.From));
        }

        return results.Min().ToString();
    }

    List<ItemRange> RangeHelper(List<NumberRange> ranges, IEnumerable<ItemRange> itemRanges)
    {
        var result = new List<ItemRange>();

        foreach (var ir in itemRanges)
        {
            ulong from = ir.From;
            ulong count = ir.Count;
            foreach (var s in ranges.OrderBy(r => r.From))
            {
                if (s.From > from + count)
                {
                    var r = from;
                    var c = Math.Min(s.From - from, count);
                    from = s.From;
                    count = count - c;
                    result.Add(new ItemRange(r, c));

                }

                else if (s.InRange(from))
                {
                    ulong r = s.Value(from);
                    ulong c = Math.Min(s.From + s.Count - from, count);
                    from += c;
                    count -= c;
                    result.Add(new ItemRange(r, c));
                }

                if (count == 0) break;
            }

            if (count > 0)
            {
                result.Add(new ItemRange(from, count));
            }
        }

        return result;
    }

}
