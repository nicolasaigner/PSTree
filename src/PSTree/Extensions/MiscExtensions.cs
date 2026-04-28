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

        internal string? GetParent()
        {
#if NET8_0_OR_GREATER
            return Path.GetDirectoryName(registryKey.Name);
#else
            string path = registryKey.Name;
            int idx = path.LastIndexOf('\\');
            if (idx == -1) return null;
            return path.Substring(0, idx);
#endif
        }
    }

    extension(TreeRegistryKey treeKey)
    {
        internal string JoinPath(string leaf) => Path.Combine(treeKey.Path, leaf);
    }
}
#endif
