using System.Text.RegularExpressions;

public static partial class Day12
{
    static readonly char[] _possibleChar = new char[] { '.', '#' };
    public static string Part1()
    {
        using var reader = new StreamReader("Day12/input.txt");
        var content = reader.ReadToEnd();
        var groups = GroupsRegex();
        var sizes = SizesRegex();
        var lines = content.Split("\r\n");
        foreach (var line in lines)
        {
            var (part1, part2, _) = line.Split(' ');
            var groupMatch = groups.Matches(part1);
            var sizeMatch = sizes.Matches(part2);
            if (groupMatch.Count == sizeMatch.Count) //c'est facile, a*...z, a à z étant le nombre de combinaisons de chaque groupe
            {
                int groupIndex = 0;
                foreach (var size in sizeMatch.Select(m => int.Parse(m.Value)))
                {
                    var value = GetAllCombinations(groupMatch[groupIndex].Value, size);
                }
            }
            else // les problèmes
            {

            }
        }
        return "";
    }

    public static string Part1_BRUTEFORCE()
    {
        using var reader = new StreamReader("Day12/input.txt");
        var lines = reader.ReadToEnd().Split("\r\n");
        var groups = GroupsRegex();
        var sizes = SizesRegex();
        var total = 0;
        foreach (var line in lines)
        {
            var (part1, part2, _) = line.Split(' ');
            IEnumerable<int> each = part1.Select((c, i) => c == '?' ? i : -1).Where(i => i != -1);
            var nMax = Math.Pow(2, each.Count());
            var matchSizes = sizes.Matches(part2).Select(m => int.Parse(m.Value)).ToList();
            var strings = new HashSet<string>();
            part1.ToCharArray().keepPrintingBitch(0, strings);
            foreach (var comb in strings)
            {
                var matches = groups.Matches(comb);
                if (matches.Count == matchSizes.Count)
                {
                    var valid = true;
                    for (int i = 0; i < matches.Count; i++)
                    {
                        if (matches[i].Value.Count(c => c == '#') != matchSizes[i])
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid)
                        total++;
                }
            }
        }
        return $"{total}";
    }

    public static string Part2_BRUTEFORCEAUSSI()
    {
        using var reader = new StreamReader("Day12/input.txt");
        var lines = reader.ReadToEnd().Split("\r\n");
        var groups = GroupsRegex();
        var sizes = SizesRegex();
        var total = 0;
        foreach (var line in lines)
        {
            var (part1, part2, _) = line.Split(' ');
            part1 = Enumerable.Repeat(part1, 5).Aggregate((x, y) => $"{x}?{y}");
            part2 = Enumerable.Repeat(part2, 5).Aggregate((x, y) => $"{x},{y}");
            var matchSizes = sizes.Matches(part2).Select(m => int.Parse(m.Value)).ToList();
            var strings = new HashSet<string>();
            part1.ToCharArray().keepPrintingBitch(0, strings);
            foreach (var comb in strings)
            {
                var matches = groups.Matches(comb);
                if (matches.Count == matchSizes.Count)
                {
                    var valid = true;
                    for (int i = 0; i < matches.Count; i++)
                    {
                        if (matches[i].Value.Count(c => c == '#') != matchSizes[i])
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid)
                        total++;
                }
            }
        }
        return $"{total}";
    }

    private static void keepPrintingBitch(this char[] str, int index, HashSet<string> strings)
    {
        if (index == str.Length)
        {
            strings.Add(new string(str));
            return;
        }
        if (str[index] == '?')
        {
            str[index] = '.';
            str.keepPrintingBitch(index + 1, strings);

            str[index] = '#';
            str.keepPrintingBitch(index + 1, strings);

            str[index] = '?';
        }
        else
        {
            str.keepPrintingBitch(index + 1, strings);
        }
    }

    private static int GetAllCombinations(string value, int size)
    {
        int comb = 1;
        return comb;
    }

    [GeneratedRegex("([\\?#]+)")]
    private static partial Regex GroupsRegex();

    [GeneratedRegex("(\\d+)")]
    private static partial Regex SizesRegex();
}
