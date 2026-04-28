namespace PSTree.Comparers;

public enum FileSystemSortMode
{
    FilesFirst,              // Default: Files first, then directories (both by name)
    DirectoriesFirst,        // Directories first, then files (both by name)
    Name,                    // Directories and files mixed, sorted by name
    Size,                    // Sort everything by size (largest first)
    DirectoriesFirstBySize,  // Directories first, then within each group by size descending
    FilesFirstBySize,        // Files first, then within each group by size descending
    None
}

#if WINDOWS
public enum RegistrySortMode
{
    ValuesFirst,  // Default: Values first, then keys (both by name)
    KeysFirst,    // Keys first, then values (both by name)
    Name,         // Keys and values mixed, sorted by name
    None
}
#endif
