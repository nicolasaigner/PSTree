#if WINDOWS
using Microsoft.Win32;

namespace PSTree.Nodes;

public sealed class TreeRegistryValue : TreeRegistryBase
{
    internal override bool Include { get; set; } = true;

    internal override bool IsContainer { get; } = false;

    public RegistryValueKind Kind { get; }

    public override string Name { get; }

    public override string PSParentPath { get; }

    internal TreeRegistryValue(
        TreeRegistryKey key,
        string value,
        string source,
        int depth)
        : base(source, $"{key.Path}:{value}", depth)
    {
        Container = key;
        Name = GetNameOrDefault(value);
        Kind = key.GetValueKind(value);
        PSParentPath = $"{ProviderPath}{key.Path}";
    }

    private static string GetNameOrDefault(string value) =>
        string.IsNullOrEmpty(value) ? "(Default)" : value;

    public object? GetValue()
    {
        string[] tokens = Path.Split([':'], 2);
        return Microsoft.Win32.Registry.GetValue(tokens[0], tokens[1], null);
    }
}
#endif
