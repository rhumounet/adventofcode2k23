public static partial class Day05
{
    public static string Part2()
    {
        using var reader = new StreamReader("Day05/input.txt");
        var content = reader.ReadToEnd();
        var i = 0;
        var pairs = new Dictionary<long, long>();
        long tempIndex = 0;
        content
            .Split(Environment.NewLine)[0]
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

        var hashset = new Dictionary<string, List<(long destination, long source, long length)>>();
        return "";
    }
}
