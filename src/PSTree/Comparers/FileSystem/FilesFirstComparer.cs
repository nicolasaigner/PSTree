using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.FileSystem;

#pragma warning disable CS8767

internal class FilesFirstComparer : IComparer<TreeFileSystemInfo>
{
    internal static FilesFirstComparer Value { get; } = new();

    public int Compare(TreeFileSystemInfo x, TreeFileSystemInfo y)
        => TreeComparisonLogic.ByLeavesFirst(x, y);
}
