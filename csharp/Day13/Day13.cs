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
            {
                total += 100 * hIndex;
            }
            else if (vCount >= hCount)
            {
                total += vIndex;
            }
        }
        return $"{total}";
    }

    public static string Part2()
    {
        var content = FileHelper.GetContent("Day13/input.txt");
        var patterns = content.Split("\r\n\r\n");
        long total = 0;
        foreach (var (pattern, index) in patterns.Select((s, i) => (s, i)))
        {
            var horizontal = pattern.Split("\r\n");
            var (hIndex, hCount) = CheckReflections(horizontal, true);
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
            var (vIndex, vCount) = CheckReflections(vertical, true);
            if (hCount >= vCount)
            {
                total += 100 * hIndex;
            }
            else if (vCount >= hCount)
            {
                total += vIndex;
            }
        }
        return $"{total}";
    }

    private static (int, int) CheckReflections(string[]? pattern, bool fix = false)
    {
        if (pattern != null)
        {
            var reflectionStarts = new List<(int l, int r)>();
            var temp = pattern[0];
            if (fix)
            {
                for (int i = 0; i < pattern.Length - 1; i++)
                {
                    for (int j = i + 1; j < pattern.Length; j++)
                    {
                        var left = pattern[i];
                        var right = pattern[j];
                        if (left.Length == right.Length)
                        {
                            List<int> indexes = new List<int>();
                            for (int k = 0; k < left.Length; k++)
                            {
                                if (left[k] != right[k]) indexes.Add(k);
                            }
                            if (indexes.Count == 1)
                            {
                                var newValue = pattern[j];
                                newValue.Remove(indexes[0]);
                                newValue.Insert(indexes[0], pattern[i][indexes[0]].ToString());
                                pattern[j] = newValue;
                            }
                        }
                    }
                }
            }
            for (int i = 1; i < pattern.Length; i++)
            {
                if (temp == pattern[i])
                {
                    reflectionStarts.Add((l: i - 1, r: i));
                }
                else temp = pattern[i];
            }
            foreach (var reflectionStart in reflectionStarts)
            {
                var keepGoing = true;
                var isReflecting = true;
                var offset = 1;
                while (keepGoing)
                {
                    if (reflectionStart.l - offset >= 0 && reflectionStart.r + offset < pattern.Length)
                    {
                        if (pattern[reflectionStart.l - offset] != pattern[reflectionStart.r + offset])
                        {
                            keepGoing = false;
                            isReflecting = false;
                        }
                        else
                        {
                            //J'aime les patates
                            offset++;
                            keepGoing = reflectionStart.l - offset >= 0 && reflectionStart.r + offset < pattern.Length;
                        }
                    }
                    else
                    {
                        keepGoing = false;
                    }
                }
                if (isReflecting)
                {
                    return (reflectionStart.r, offset);
                }
            }
        }
        return (-1, -1);
    }
}
