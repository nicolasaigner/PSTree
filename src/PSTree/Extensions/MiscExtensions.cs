#if WINDOWS
using System.IO;
using Microsoft.Win32;
using PSTree.Nodes;

namespace PSTree.Extensions;

internal static class MiscExtensions
{
    extension(string[] values)
    {
        internal void Deconstruct(out string baseKey, out string? subKey)
        {
            baseKey = values[0];
            subKey = values.Length == 1 ? null : values[1];
        }
    }

    extension(string value)
    {

    }


    extension(RegistryKey registryKey)
    {
        internal string GetName() => Path.GetFileName(registryKey.Name);

        internal string? GetParent() => Path.GetDirectoryName(registryKey.Name);
    }

    extension(TreeRegistryKey treeKey)
    {
        internal string JoinPath(string leaf) => Path.Combine(treeKey.Name, leaf);
    }
}
#endif
