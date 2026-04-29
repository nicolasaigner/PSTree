#if WINDOWS
namespace PSTree.Nodes;

public abstract class TreeRegistryBase(string source, string path, int depth = 0)
    : TreeBase<TreeRegistryKey, TreeRegistryBase>(source, depth)
{
    protected const string ProviderPath = @"Microsoft.PowerShell.Core\Registry::";

    public abstract string PSParentPath { get; }
    public abstract string PSPath { get; }
    public string Path { get; } = path;
}
#endif
