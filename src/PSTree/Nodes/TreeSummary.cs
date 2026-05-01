namespace PSTree.Nodes;

public sealed class TreeSummary : TreeFileSystemInfo
{
    public override string Mode { get; } = "-----";

    public override string Name { get; }

    internal override bool IsContainer { get; } = false;

    internal TreeSummary(TreeDirectory parent, int dirCount, int fileCount)
        : base(parent.Source, parent.Depth + 1)
    {
        Container = parent;
        Name = GetName(dirCount, fileCount);
    }

    private static string GetName(int dirCount, int fileCount)
    {
        const string d = "folder";
        const string f = "file";

        static string Label(int count, string noun)
            => count == 1 ? $"{count} {noun}" : $"{count} {noun}s";

        return (dirCount, fileCount) switch
        {
            ( > 0, > 0) => $"[+{Label(dirCount, d)}, +{Label(fileCount, f)}]",
            ( > 0,   0) => $"[+{Label(dirCount, d)}]",
            _           => $"[+{Label(fileCount, f)}]"
        };
    }
}
