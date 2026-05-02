namespace PSTree.Nodes;

public sealed class TreeSummary : TreeFileSystemInfo
{
    public override string Mode { get; } = "-----";

    public override string Name { get; }

    internal override bool IsContainer { get; } = false;

    internal TreeSummary(TreeDirectory parent, int dCount, int fCount)
        : base(parent.Source, parent.Depth + 1)
    {
        Container = parent;
        Name = GetName(dCount, fCount);
    }

    private static string GetName(int dCount, int fCount)
    {
        const string d = "folder";
        const string f = "file";

        static string Label(int count, string noun)
            => count == 1 ? $"{count} {noun}" : $"{count} {noun}s";

        return (dCount, fCount) switch
        {
            ( > 0, > 0) => $"[+{Label(dCount, d)}, +{Label(fCount, f)}]",
            ( > 0,   0) => $"[+{Label(dCount, d)}]",
            _           => $"[+{Label(fCount, f)}]"
        };
    }
}
