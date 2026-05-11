#if WINDOWS
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Win32;
using Win32Registry = Microsoft.Win32.Registry;

namespace PSTree.Registry;

internal static class RegistryMappings
{
    private static readonly Dictionary<string, RegistryKey> s_map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["HKEY_CURRENT_USER"] = Win32Registry.CurrentUser,
        ["HKEY_LOCAL_MACHINE"] = Win32Registry.LocalMachine,
        ["HKEY_CLASSES_ROOT"] = Win32Registry.ClassesRoot,
        ["HKEY_USERS"] = Win32Registry.Users,
        ["HKEY_CURRENT_CONFIG"] = Win32Registry.CurrentConfig
    };

    internal static RegistryKey Get(string key) => s_map[key];
}
#endif
