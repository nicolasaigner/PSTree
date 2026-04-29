#if WINDOWS
using Microsoft.Win32;

namespace PSTree.Nodes;

public sealed class TreeRegistryValue : TreeRegistryBase
{
    private readonly string _name;

    internal override bool Include { get; set; } = true;

    internal override bool IsContainer { get; } = false;

    public override string PSParentPath { get; }

    public override string PSPath { get; } = string.Empty;

    public RegistryValueKind Kind { get; }

    public override string Name { get; }

    internal TreeRegistryValue(
        TreeRegistryKey key,
        string name,
        string source,
        int depth)
        : base(source, $"{key.Path}\\{GetNameOrDefault(name)}", depth)
    {
        _name = name;
        Container = key;
        Name = GetNameOrDefault(name);
        Kind = key.GetValueKind(name);
        PSParentPath = $"{ProviderPath}{key.Path}";
    }

    private static string GetNameOrDefault(string name)
        => string.IsNullOrEmpty(name) ? "(Default)" : name;

    public object? GetValue() => Container?.GetValue(_name);
}
#endif
