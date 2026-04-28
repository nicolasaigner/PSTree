using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.FileSystem;

#pragma warning disable CS8767

internal class DirectoriesFirstBySizeComparer : IComparer<TreeFileSystemInfo>
{
    internal static DirectoriesFirstBySizeComparer Value { get; } = new();

    public int Compare(TreeFileSystemInfo x, TreeFileSystemInfo y)
    {
        if (x.IsContainer != y.IsContainer)
            return x.IsContainer ? -1 : 1;

        return TreeComparisonLogic.BySize(x, y);
    }
}
