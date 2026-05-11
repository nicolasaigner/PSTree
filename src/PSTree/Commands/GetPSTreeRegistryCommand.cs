#if WINDOWS
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using Microsoft.Win32;
using PSTree.Comparers;
using PSTree.Extensions;
using PSTree.Nodes;
using PSTree.Registry;
using System.Diagnostics.CodeAnalysis;

namespace PSTree.Commands;

[Cmdlet(VerbsCommon.Get, "PSTreeRegistry", DefaultParameterSetName = PathSet)]
[OutputType(typeof(TreeRegistryKey), typeof(TreeRegistryValue))]
[Alias("pstreereg")]
public sealed class GetPSTreeRegistryCommand
    : TreeCommandBase<TreeRegistryKey, TreeRegistryBase, RegistrySortMode>
{
    [Parameter]
    [Alias("k", "key")]
    public SwitchParameter KeysOnly { get; set; }

    protected override void ProcessRecord()
    {
        foreach ((ProviderInfo provider, string path) in EnumerateResolvedPaths())
        {
            if (provider.Name != "Registry")
            {
                WriteError(provider.ToInvalidProviderError(path, expected: "Registry"));
                continue;
            }

            if (!TryGetKey(path, out RegistryKey? key))
                continue;

            ProcessTree(new TreeRegistryKey(key));
        }
    }

    protected override void BuildOne(TreeRegistryKey current, int depth)
    {
        bool hasValue = false;
        string source = current.Source;

        using (current)
        {
            if (depth > Depth) return;
            if (!KeysOnly)
            {
                foreach (string value in current.GetValueNames())
                {
                    if (!ShouldSkipValue(value))
                    {
                        current.AddValue(value, source);
                        hasValue = true;
                    }
                }
            }

            foreach (string name in current.EnumerateKeys())
            {
                if (ShouldExclude(name)) continue;

                try
                {
                    if (current.TryAddSubKey(name, source, out TreeRegistryKey? subk))
                        Push(subk);
                }
                catch (SecurityException exception)
                {
                    string path = current.JoinPath(name);
                    WriteError(exception.ToSecurityError(path));
                }
            }

            if (HasInclude && hasValue)
                current.PropagateInclude();
        }
    }

    private bool ShouldSkipValue(string value) => ShouldExclude(value) || !ShouldInclude(value);

    private bool TryGetKey(string path, [NotNullWhen(true)] out RegistryKey? key)
    {
        (string baseKey, string? subKey) = path.Split(['\\'], 2);
        key = RegistryMappings.Get(baseKey);
        if (string.IsNullOrWhiteSpace(subKey)) return true;

        try
        {
            key = key.OpenSubKey(subKey);
            if (key is not null) return true;
            this.WriteInvalidPathError(path);
        }
        catch (SecurityException exception)
        {
            WriteError(exception.ToSecurityError(path));
            key = null;
        }

        return false;
    }

    protected override IComparer<TreeRegistryBase>? GetComparer()
        => TreeComparerFactory.Get(SortBy);
}
#endif
