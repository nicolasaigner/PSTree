using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.FileSystem;

#pragma warning disable CS8767

internal class DirectoriesFirstComparer : IComparer<TreeFileSystemInfo>
{
    internal static DirectoriesFirstComparer Value { get; } = new();

    public int Compare(TreeFileSystemInfo x, TreeFileSystemInfo y)
        => TreeComparisonLogic.ByContainersFirst(x, y);
}
