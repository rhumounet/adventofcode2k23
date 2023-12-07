
enum HandType
{
    FiveOfKind = 6,
    FourOfKind = 5,
    FullHouse = 4,
    ThreeOfKind = 3,
    TwoPairs = 2,
    Pair = 1,
    HighCard = 0
}

class Hand : IComparable<Hand>
{
    public Hand(string value, int bid, HandType type, bool enableJokers = false)
    {
        Value = value;
        Bid = bid;
        Type = type;
        EnableJokers = enableJokers;
    }

    public string Value { get; set; }
    public int Bid { get; set; }
    public HandType Type { get; set; }
    public bool EnableJokers { get; set; }

    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            return -1;
        }
        if (Type != other.Type)
        {
            return Type > other.Type ? -1 : 1;
        }
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i] == other.Value[i])
                continue;
            else
                return EnableJokers ?
                    (Day07.RankingWithJokers[Value[i]] > Day07.RankingWithJokers[other.Value[i]] ? -1 : 1)
                    :
                    (Day07.Ranking[Value[i]] > Day07.Ranking[other.Value[i]] ? -1 : 1);
        }
        return 0;
    }

    public static HandType GetHandType(string value, bool enableJokers = false)
    {
        if (enableJokers && value.Any(c => c == 'J') && value != "JJJJJ")
        {
            var highest = value
                .Where(c => c != 'J')
                .GroupBy(c => c)
                .Select(g => (g.Key, Count: g.Count()))
                .OrderByDescending(g => g.Count)
                .ThenBy(g => Day07.Ranking[g.Key])
                .FirstOrDefault().Key;
            value = value.Replace('J', highest);
        }
        var each = value.GroupBy(c => c).ToList();
        if (each.Count == 1)
            return HandType.FiveOfKind;
        if (each.Any(g => g.Count() == 4))
            return HandType.FourOfKind;
        if (each.Count == 2 && each.Any(g => g.Count() == 3))
            return HandType.FullHouse;
        if (each.Any(g => g.Count() == 3))
            return HandType.ThreeOfKind;
        if (each.Count == 3)
            return HandType.TwoPairs;
        if (each.Count == 4)
            return HandType.Pair;
        return HandType.HighCard;
    }
}