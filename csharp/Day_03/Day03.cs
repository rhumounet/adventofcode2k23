using System.Text.RegularExpressions;

public static partial class Day03
{
    public static string Part1()
    {
        using var reader = new StreamReader("Day_03/input.txt");
        var lines = reader.ReadToEnd();
        var regex = new Regex(@$"(\d+)+", RegexOptions.Multiline);
        var lineNumber = 0;
        var array = lines.Split(Environment.NewLine).Select(s => s.ToCharArray()).ToArray();
        var maxLine = array[0].Length;
        var matches = regex.Matches(lines.Replace(Environment.NewLine, ""));
        var sum = 0;
        foreach (Match match in matches)
        {
            lineNumber = match.Index / maxLine;
            var realIndex = match.Index % maxLine;
            var anySpecialChar = false;
            for (int i = lineNumber - 1; i <= lineNumber + 1; i++)
            {
                for (int j = realIndex - 1; j < realIndex + match.Length + 1; j++)
                {
                    if (i >= 0 && j >= 0 && j < maxLine && i < array.Length)
                    {
                        var @char = array[i][j];
                        if (array[i][j] != '.' && !int.TryParse($"{@char}", out _))
                        {
                            anySpecialChar = true;
                            break;
                        }
                    }
                }
            }
            if (anySpecialChar) sum += int.Parse(match.Value);   
        }
        return $"{sum}";
    }

    public static string Part2()
    {
        using var reader = new StreamReader("Day_03/input.txt");
        var lines = reader.ReadToEnd();
        var regex = Numeral();
        var lineNumber = 0;
        var array = lines.Split(Environment.NewLine).Select(s => s.ToCharArray()).ToArray();
        var maxLine = array[0].Length;
        var matches = regex.Matches(lines.Replace(Environment.NewLine, ""));
        var dict = new Dictionary<(int x, int y), int[]>();
        foreach (Match match in matches)
        {
            lineNumber = match.Index / maxLine;
            var realIndex = match.Index % maxLine;
            for (int i = lineNumber - 1; i <= lineNumber + 1; i++)
            {
                for (int j = realIndex - 1; j < realIndex + match.Length + 1; j++)
                {
                    if (i >= 0 && j >= 0 && j < maxLine && i < array.Length)
                    {
                        var @char = array[i][j];
                        if (array[i][j] == '*')
                        {
                            if (dict.ContainsKey((i, j)))
                                dict[(i, j)][1] = int.Parse(match.Value);
                            else
                                dict.Add((i, j), new int[2] { int.Parse(match.Value), 0 });
                            break;
                        }
                    }
                }
            }
        }
        return $"{dict.Values.Sum(x => x.Aggregate((i1, i2) => i1* i2))}";
    }

    [GeneratedRegex("(\\d+)+", RegexOptions.Multiline)]
    private static partial Regex Numeral();
}
