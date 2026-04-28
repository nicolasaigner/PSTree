using PSTree.Interfaces;

namespace PSTree.Comparers;

internal static class TreeComparisonLogic
{
    internal static int ByName<T>(T x, T y)
        where T : ITree
    {
        return x.Name.CompareTo(y.Name);
    }

    internal static int BySize<T>(T x, T y)
        where T : IFileSystemNode
    {
        return y.Length.CompareTo(x.Length);
    }

    internal static int ByContainersFirst<T>(T x, T y)
        where T : ITree
    {
        if (x.IsContainer != y.IsContainer)
            return x.IsContainer ? -1 : 1;

        return ByName(x, y);
    }

    internal static int ByLeavesFirst<T>(T x, T y)
        where T : ITree
    {
        if (x.IsContainer != y.IsContainer)
            return x.IsContainer ? 1 : -1;

        return ByName(x, y);
    }
}
