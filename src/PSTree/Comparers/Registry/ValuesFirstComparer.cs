#if WINDOWS
using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.Registry;

#pragma warning disable CS8767

internal class ValuesFirstComparer : IComparer<TreeRegistryBase>
{
    internal static ValuesFirstComparer Value { get; } = new();

    public int Compare(TreeRegistryBase x, TreeRegistryBase y)
        => TreeComparisonLogic.ByLeavesFirst(x, y);
}
#endif
