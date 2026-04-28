using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.FileSystem;

#pragma warning disable CS8767

internal class SizeComparer : IComparer<TreeFileSystemInfo>
{
    internal static SizeComparer Value { get; } = new();

    public int Compare(TreeFileSystemInfo x, TreeFileSystemInfo y)
        => TreeComparisonLogic.BySize(x, y);
}
