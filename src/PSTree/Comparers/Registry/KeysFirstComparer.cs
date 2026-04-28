#if WINDOWS
using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.Registry;

#pragma warning disable CS8767

internal class KeysFirstComparer : IComparer<TreeRegistryBase>
{
    internal static KeysFirstComparer Value { get; } = new();

    public int Compare(TreeRegistryBase x, TreeRegistryBase y)
        => TreeComparisonLogic.ByContainersFirst(x, y);
}
#endif
