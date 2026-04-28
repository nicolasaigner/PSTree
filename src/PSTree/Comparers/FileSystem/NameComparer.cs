using System.Collections.Generic;
using PSTree.Nodes;

namespace PSTree.Comparers.FileSystem;

#pragma warning disable CS8767

internal class FileSystemNameComparer : IComparer<TreeFileSystemInfo>
{
    internal static FileSystemNameComparer Value { get; } = new();

    public int Compare(TreeFileSystemInfo x, TreeFileSystemInfo y)
        => TreeComparisonLogic.ByName(x, y);
}
