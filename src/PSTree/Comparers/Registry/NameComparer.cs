#if WINDOWS
using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.Registry;

#pragma warning disable CS8767

internal class RegistryNameComparer : IComparer<TreeRegistryBase>
{
    internal static RegistryNameComparer Value { get; } = new();

    public int Compare(TreeRegistryBase x, TreeRegistryBase y)
        => TreeComparisonLogic.ByName(x, y);
}
#endif
