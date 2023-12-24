public static partial class Day05
{
    public static string Part2()
    {
        using var reader = new StreamReader("Day05/input.txt");
        var content = reader.ReadToEnd();
        var pairs = new Dictionary<long, long>();
        var lines = content.Split(Environment.NewLine);
        var i = 0;
        long tempIndex = 0;
        lines[0]
            .Split(":")[1]
            .Trim()
            .Split(" ").ToList().ForEach(s =>
            {
                if (i % 2 == 0)
                    pairs.Add((tempIndex = long.Parse(s)), 0);
                else
                    pairs[tempIndex] = long.Parse(s);
                i++;
            });

        var ranges = new Dictionary<string, List<Range>>();
        var currentCat = "";
        string[] currentLine;
        for (int j = 2; j < lines.Length; j++)
        {
            if (lines[j].Contains("map:"))
            {
                currentCat = lines[j].Split(" ")[0];
                ranges.Add(currentCat, new List<Range>());
            }
            else if (!string.IsNullOrEmpty(lines[j]))
            {
                currentLine = lines[j].Split(" ");
                ranges[currentCat].Add(new Range(long.Parse(currentLine[0]), long.Parse(currentLine[1]), long.Parse(currentLine[2])));
            }
        }
        return "";
    }

    public class Range
    {
        public long destination;
        public long source;
        public long length;
        public long min_dest => destination;
        public long max_dest => destination + length;

        public long min_source => source;
        public long max_source => source + length;

        public Range(long destination, long source, long length)
        {
            this.destination = destination;
            this.source = source;
            this.length = length;
        }

        //public (Range dest_intersection, Range src_intersection, Range[] rest) Intersection(Range other)
        //{
        //    Range dest_intersection, src_intersection;
        //    Range[] rest;
        //    if (min_dest < other.max_source|| max_dest > other.min_source)
        //    {
        //        dest_intersection = new Range(Math.Max(min_dest, other.min_source), Math.Min(max_dest, other.max_source));
        //    }
        //}
    }

}
