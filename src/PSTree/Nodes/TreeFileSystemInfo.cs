using PSTree.Interfaces;
using PSTree.Internal;

namespace PSTree.Nodes;

public abstract class TreeFileSystemInfo(string source, int depth = 0)
    : TreeBase<TreeDirectory, TreeFileSystemInfo>(source, depth), IFileSystemNode
{
    public abstract string Mode { get; }

    public long Length { get; internal set; }

    public string GetFormattedLength()
        => _FormattingInternals.GetFormattedLength(Length);

    internal void RecursiveDecrement()
    {
        TreeDirectory? i = Container;
        if (i is null) return;

        i.ItemCount--;
        i.TotalItemCount--;

        for (i = i.Container; i is not null; i = i.Container)
            i.TotalItemCount--;
    }
}
