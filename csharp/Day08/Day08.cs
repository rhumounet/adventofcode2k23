using System.Diagnostics;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

public static partial class Day08
{
    public static string Part1()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        using var reader = new StreamReader("Day08/input.txt");
        var content = reader.ReadToEnd();
        var instructions = content.Split(Environment.NewLine)[0];
        var nodeRegex = NodeRegex();
        var nodes = content.Split(Environment.NewLine).Skip(2).Select(x =>
        {
            var result = nodeRegex.Match(x);
            return (Source: result.Groups[1].Value, Left: result.Groups[2].Value, Right: result.Groups[3].Value);
        }).ToDictionary(x => x.Source, x => (x.Left, x.Right));
        var currentKey = "AAA";
        var currentNode = nodes[currentKey ?? ""];
        var index = 0;
        long steps = 0;
        while (currentKey != "ZZZ")
        {
            if (index == instructions.Length)
                index = 0;
            currentKey = instructions[index] switch
            {
                'L' => currentNode.Left,
                'R' => currentNode.Right,
                _ => throw new Exception("?")
            };
            currentNode = nodes[currentKey];
            index++;
            steps++;
        }
        stopwatch.Stop();
        return $"{steps} - executed in {stopwatch.ElapsedMilliseconds}ms";
    }

    public static string Part2()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        using var reader = new StreamReader("Day08/input.txt");
        var content = reader.ReadToEnd();
        var instructions = content.Split(Environment.NewLine)[0];
        var nodeRegex = NodeRegex();
        var nodes = content.Split(Environment.NewLine).Skip(2).Select(x =>
        {
            var result = nodeRegex.Match(x);
            return (Source: result.Groups[1].Value, Left: result.Groups[2].Value, Right: result.Groups[3].Value);
        }).ToDictionary(x => x.Source, x => (x.Left, x.Right));
        var currentKeys = nodes.Keys.Where(n => n.EndsWith('A')).ToList();
        var currentNodes = currentKeys.Select(k => nodes[k]).ToList();
        var index = 0;
        long steps = 0;
        bool keepGoing = true;
        var stepsByKey = new Dictionary<string, long>();
        while (keepGoing)
        {
            var instruction = instructions[index % instructions.Length];
            currentKeys = currentNodes.Select(n => instruction switch
            {
                'L' => n.Left,
                'R' => n.Right,
                _ => throw new Exception("?")
            }).ToList();
            string? found;
            currentNodes = currentKeys.Select(k => nodes[k]).ToList();
            index++;
            steps++;
            if (!string.IsNullOrEmpty(found = currentKeys.FirstOrDefault(k => k.EndsWith("Z"))))
            {
                if (found != null && !stepsByKey.ContainsKey(found))
                {
                    stepsByKey.Add(found, steps);
                }
            }
            keepGoing = stepsByKey.Count < currentKeys.Count;
        }
        stopwatch.Stop();
        return $"{LCM(stepsByKey.Values.ToArray())} - executed in {stopwatch.ElapsedMilliseconds}ms";
    }

    public static long LCM(long[] numbers)
    {
        return numbers.Aggregate(LCM);
    }

    public static long LCM(long x, long y)
    {
        return x * y / GCD(x, y);
    }

    public static long GCD(long x, long y)
    {
        return x % y == 0 ? y : GCD(y, x % y);
    }

    [GeneratedRegex("([0-9A-Z]{3})\\s\\=\\s\\(([0-9A-Z]{3}),\\s([0-9A-Z]{3})\\)")]
    private static partial Regex NodeRegex();
}
