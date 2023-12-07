public static partial class Day07
{
    public static readonly Dictionary<char, int> Ranking = new Dictionary<char, int>()
    {
        { 'A', 1 },
        { 'K', 2 },
        { 'Q', 3 },
        { 'J', 4 },
        { 'T', 5 },
        { '9', 6 },
        { '8', 7 },
        { '7', 8 },
        { '6', 9 },
        { '5', 10 },
        { '4', 11 },
        { '3', 12 },
        { '2', 13 }
    };
    public static readonly Dictionary<char, int> RankingWithJokers = new Dictionary<char, int>()
    {
        { 'A', 1 },
        { 'K', 2 },
        { 'Q', 3 },
        { 'T', 5 },
        { '9', 6 },
        { '8', 7 },
        { '7', 8 },
        { '6', 9 },
        { '5', 10 },
        { '4', 11 },
        { '3', 12 },
        { '2', 13 },
        { 'J', 14 }
    };
    public static string Part1()
    {
        using var reader = new StreamReader("Day07/input.txt");
        var content = reader.ReadToEnd();
        var result = content
            .Split(Environment.NewLine)
            .Select(l => new Hand(
                value: l.Split(' ')[0], 
                bid: int.Parse(l.Split(' ')[1]), 
                type: Hand.GetHandType(l.Split(' ')[0])))
            .GroupBy(h => h.Type)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                g.Key,
                Ordered = g.OrderBy(x => x).ToList()
            })
            .SelectMany(g => g.Ordered)
            .Select((h, i) => h.Bid * (i + 1))
            .Sum();
        return $"{result}";
    }

    
    public static string Part2()
    {
        using var reader = new StreamReader("Day07/input.txt");
        var content = reader.ReadToEnd();
        var result = content
            .Split(Environment.NewLine)
            .Select(l => new Hand(
                value: l.Split(' ')[0],
                bid: int.Parse(l.Split(' ')[1]),
                type: Hand.GetHandType(l.Split(' ')[0], true),
                enableJokers: true))
            .GroupBy(h => h.Type)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                g.Key,
                Ordered = g.OrderBy(x => x).ToList()
            })
            .SelectMany(g => g.Ordered)
            .Select((h, i) => h.Bid * (i + 1))
            .Sum();
        return $"{result}";
    }
}
