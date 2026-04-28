#if WINDOWS
using Microsoft.Win32;
using PSTree.Extensions;

namespace PSTree.Nodes;

public sealed class TreeRegistryValue : TreeRegistryBase
{
    internal override bool Include { get; set; } = true;

    internal override bool IsContainer { get; } = false;

    public override string PSParentPath { get; }

    public override string PSPath { get; } = string.Empty;

    public RegistryValueKind Kind { get; }

    public override string Name { get; }

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
        (string path, string? value) = Path.Split([':'], 2);
        return Microsoft.Win32.Registry.GetValue(path, value, null);
    }
}
#endif
