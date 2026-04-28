#if WINDOWS
using PSTree.Extensions;

namespace PSTree.Nodes;

public abstract class TreeRegistryBase(string source, string path, int depth = 0)
    : TreeBase<TreeRegistryKey, TreeRegistryBase>(source, depth)
{
    protected const string ProviderPath = @"Microsoft.PowerShell.Core\Registry::";

    public string Path { get; } = path;

    public string PSPath { get; } = $"{ProviderPath}{path}";

    public virtual string PSParentPath { get; } = $"{ProviderPath}{path.GetParent()}";
}
#endif
