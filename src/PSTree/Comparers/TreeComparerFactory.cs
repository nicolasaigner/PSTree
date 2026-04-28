using System.Collections.Generic;
using PSTree.Comparers.FileSystem;
using PSTree.Comparers.Registry;
using PSTree.Nodes;

namespace PSTree.Comparers;

internal static class TreeComparerFactory
{
    internal static IComparer<TreeFileSystemInfo>? Get(FileSystemSortMode mode) => mode switch
    {
        FileSystemSortMode.FilesFirst => FilesFirstComparer.Value,
        FileSystemSortMode.DirectoriesFirst => DirectoriesFirstComparer.Value,
        FileSystemSortMode.Name => FileSystemNameComparer.Value,
        FileSystemSortMode.Size => SizeComparer.Value,
        FileSystemSortMode.DirectoriesFirstBySize => DirectoriesFirstBySizeComparer.Value,
        FileSystemSortMode.FilesFirstBySize => FilesFirstBySizeComparer.Value,
        _ => null // FileSystemSortMode.None
    };

#if WINDOWS
    internal static IComparer<TreeRegistryBase>? Get(RegistrySortMode mode) => mode switch
    {
        RegistrySortMode.ValuesFirst => ValuesFirstComparer.Value,
        RegistrySortMode.KeysFirst => KeysFirstComparer.Value,
        RegistrySortMode.Name => RegistryNameComparer.Value,
        _ => null // RegistrySortMode.None
    };
#endif
}
