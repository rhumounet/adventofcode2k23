public static class Extensions
{
    public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
    {
        first = list.Count > 0 ? list[0] : default(T);
        rest = list.Skip(1).ToList();
    }

    public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
    {
        first = list.Count > 0 ? list[0] : default(T);
        second = list.Count > 1 ? list[1] : default(T);
        rest = list.Skip(2).ToList();
    }

    public static void Deconstruct<T, G>(this IGrouping<T, G> grouping, out G first, out IList<G> rest)
    {
        first = grouping.Count() > 0 ? grouping.FirstOrDefault() : default(G);
        rest = grouping.Skip(1).ToList();
    }

    public static void Deconstruct<T, G>(this IGrouping<T, G> grouping, out G first, out G second, out IList<G> rest)
    {
        first = grouping.Count() > 0 ? grouping.FirstOrDefault() : default(G);
        second = grouping.Count() > 1 ? grouping.Skip(1).FirstOrDefault() : default(G);
        rest = grouping.Skip(2).ToList();
    }
}