using System.Diagnostics;
using csharp;

public static class Day13
{
    public static string Part1()
    {
        var content = FileHelper.GetContent("Day13/input.txt");
        var patterns = content.Split("\r\n\r\n");
        long total = 0;
        foreach (var (pattern, index) in patterns.Select((s, i) => (s, i)))
        {
            var horizontal = pattern.Split("\r\n");
            var (hIndex, hCount) = CheckReflections(horizontal);
            //transform horizontal to vertical to ease treatments
            var vertical = new string[horizontal[0].Length];
            for (int x = 0; x < horizontal[0].Length; x++)
            {
                var temp = "";
                for (int y = 0; y < horizontal.Length; y++)
                {
                    temp += horizontal[y][x];
                }
                vertical[x] = temp;
            }
            var (vIndex, vCount) = CheckReflections(vertical);
            if (hCount >= vCount)
                total += 100 * hIndex;
            else if (vCount >= hCount)
                total += vIndex;
        }
        return $"{total}";
    }

    private static (int, int) CheckReflections(string[]? pattern)
    {
        if (pattern != null)
        {
            var group = pattern
                .Select((s, i) => (s, i))
                .GroupBy(g => g.s);
            var bounds = group
                .FirstOrDefault(g => g.Count() > 1 && g.Any(g => g.i == 0 || g.i == pattern.Length - 1));
            if (bounds != null)
            {
                (string s, int i) firstBound = default, lastBound = default;
                var list = bounds.ToList();
                if (bounds.Count() % 2 != 0) //casse les couilles les cas à la con putain de chiasse
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var current = list[i];
                        for (int j = i + 1; j < list.Count; j++)
                        {
                            if ((((current.i + list[j].i) + 1) / 2) % 2 == 0)
                            {
                                firstBound = current;
                                lastBound = list[j];
                                break;
                            }
                        }
                        if (firstBound != default && lastBound != default)
                        {
                            break;
                        }
                    }
                } else
                {
                    firstBound = bounds.FirstOrDefault();
                    lastBound = bounds.LastOrDefault();
                }
                var reflecting = group.Where(g => g.Count() > 1 && g.FirstOrDefault().i >= firstBound.i && g.LastOrDefault().i <= lastBound.i).SelectMany(g => g).Count();
                int reflectLine = (firstBound.i + lastBound.i + 1) / 2;
                return (reflectLine, reflecting);
            }
        }
        return (0, 0);
    }
}
