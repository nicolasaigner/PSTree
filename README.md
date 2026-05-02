<h1 align="center">PSTree</h1>

<div align="center">
   <sub>

   `tree` like cmdlets for PowerShell!
   </sub>
<br/><br/>

[![build](https://github.com/santisq/PSTree/actions/workflows/ci.yml/badge.svg)](https://github.com/santisq/PSTree/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/santisq/PSTree/branch/main/graph/badge.svg?token=b51IOhpLfQ)](https://codecov.io/gh/santisq/PSTree)
[![PowerShell Gallery](https://img.shields.io/powershellgallery/dt/PSTree?color=%23008FC7)](https://www.powershellgallery.com/packages/PSTree)
[![LICENSE](https://img.shields.io/github/license/santisq/PSTree)](https://github.com/santisq/PSTree/blob/main/LICENSE)

</div>

PSTree is a PowerShell module that extends tree-style navigation to both file systems and the Windows Registry through two versatile cmdlets. Designed for administrators, developers, and power users, it combines hierarchical visualization with practical insights like folder sizes and registry traversal.

## Cmdlets

- **`Get-PSTree`**  
  Inspired by the classic [`tree` command](https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/tree), this cmdlet displays your file system in a structured hierarchy. It goes further by calculating folder sizes—both individual and recursive—making it a powerful tool for disk usage analysis.

- **`Get-PSTreeRegistry`** *(Windows only)*  
  Explore the Windows Registry with a tree-like view of keys and values. This cmdlet simplifies navigation and troubleshooting by presenting registry structures in an intuitive format, ideal for system configuration tasks.

- **`Get-PSTreeStyle`**  
  Retrieves the singleton `TreeStyle` instance used to customize the colored, hierarchical output of `Get-PSTree` and `Get-PSTreeRegistry`.  
  Allows you to change colors for directories, files (including by extension), registry keys, and registry value kinds, as well as apply accents and control ANSI output rendering.

## Documentation

- Learn how to use the cmdlets in the [official documentation](./docs/en-US/).

- To Customize output rendering, see [about_TreeStyle](./docs/en-US/about_TreeStyle.md).

## Installation

### Gallery

The module is available through the [PowerShell Gallery](https://www.powershellgallery.com/packages/PSTree):

```powershell
Install-Module PSTree -Scope CurrentUser
```

### Source

```powershell
git clone 'https://github.com/santisq/PSTree.git'
Set-Location ./PSTree
./build.ps1
```

## Requirements

- **Windows PowerShell v5.1** or [**PowerShell 7+**](https://github.com/PowerShell/PowerShell).
- **Windows OS** (for `Get-PSTreeRegistry`).

## Usage

### `Get-PSTree`

#### Get the current directory tree with default parameters values

```powershell
PS \> Get-PSTree

   Source: D:\pwsh\PSTree

Mode            Length Hierarchy
----            ------ ---------
d----         37.54 KB PSTree
-a---          4.75 KB ├── .gitignore
-a---        137.00  B ├── .markdownlint.json
-a---          1.37 KB ├── build.ps1
-a---         19.73 KB ├── CHANGELOG.md
-a---          1.07 KB ├── LICENSE
-a---         10.48 KB ├── README.md
d----          0.00  B ├── .github
d----          4.10 KB │   └── workflows
-a---          4.10 KB │       └── ci.yml
d----          4.18 KB ├── .vscode
-a---        275.00  B │   ├── extensions.json
-a---          1.39 KB │   ├── launch.json
-a---          1.09 KB │   ├── settings.json
-a---          1.43 KB │   └── tasks.json
d----        491.24 KB ├── assets
-a---         95.47 KB │   ├── ClassicStyles.png
-a---         96.12 KB │   ├── FancyStyles.png
-a---         66.76 KB │   ├── Get-PSTree.After.png
-a---         63.05 KB │   ├── Get-PSTree.Before.png
...
```

#### Excludes items starting with `.g`, `.v`, `.m` and `assets`

```powershell
PS \> Get-PSTree -Exclude .[gvm]*, assets

   Source: D:\pwsh\PSTree

Mode            Length Hierarchy
----            ------ ---------
d----         32.65 KB PSTree
-a---          1.37 KB ├── build.ps1
-a---         19.73 KB ├── CHANGELOG.md
-a---          1.07 KB ├── LICENSE
-a---         10.48 KB ├── README.md
d----          0.00  B ├── docs
d----         36.81 KB │   └── en-US
-a---          6.68 KB │       ├── about_TreeStyle.md
-a---         16.90 KB │       ├── Get-PSTree.md
-a---         11.84 KB │       ├── Get-PSTreeRegistry.md
-a---          1.39 KB │       └── Get-PSTreeStyle.md
d----         22.39 KB ├── module
-a---         17.25 KB │   ├── PSTree.Format.ps1xml
-a---          5.14 KB │   └── PSTree.psd1
d----        106.22 KB ├── output
-a---        106.22 KB │   ├── PSTree.3.0.0.nupkg
d----          0.00  B │   ├── PSTree
d----          0.00  B │   │   └── 3.0.0
d----        277.39 KB │   └── TestResults
-a---        249.51 KB │       ├── Coverage.xml
...
```

#### Includes `.ps1` and `.cs` files, excludes `tools` folder and sorts items by size

```powershell
PS \> Get-PSTree -Depth 4 -Include *.ps1, *.cs -Exclude tools -SortBy Size

   Source: D:\pwsh\PSTree

Mode            Length Hierarchy
----            ------ ---------
d----          1.37 KB PSTree
d----         29.54 KB ├── tests
-a---          9.29 KB │   ├── TreeStyle.tests.ps1
-a---          7.35 KB │   ├── GetPSTreeRegistryCommand.tests.ps1
-a---          7.33 KB │   ├── GetPSTreeCommand.tests.ps1
-a---          2.46 KB │   ├── TreeFileSystemInfo_T.tests.ps1
-a---          1.59 KB │   ├── TreeDirectory.tests.ps1
-a---        804.00  B │   ├── FormattingInternals.tests.ps1
-a---        745.00  B │   └── TreeFile.tests.ps1
-a---          1.37 KB ├── build.ps1
d----          0.00  B └── src
d----          0.00  B     └── PSTree
d----         13.73 KB         ├── Nodes
-a---          3.61 KB         │   ├── TreeBase.cs
-a---          2.55 KB         │   ├── TreeRegistryKey.cs
-a---          2.30 KB         │   ├── TreeDirectory.cs
-a---          1.43 KB         │   ├── TreeFileSystemInfo_T.cs
-a---          1.15 KB         │   ├── TreeRegistryValue.cs
-a---        967.00  B         │   ├── TreeSummary.cs
-a---        714.00  B         │   ├── TreeFileSystemInfo.cs
...
```

#### Get the recursive size of the folders

```powershell
PS \> Get-PSTree .\src -Depth 2 -Directory -RecursiveSize

   Source: D:\pwsh\PSTree\src

Mode            Length Hierarchy
----            ------ ---------
d----          1.15 MB src
d----          1.15 MB └── PSTree
d----        732.37 KB     ├── bin
d----          7.81 KB     ├── CodeAnalysis
d----         13.17 KB     ├── Commands
d----          6.51 KB     ├── Comparers
d----          7.50 KB     ├── Extensions
d----        331.00  B     ├── Interfaces
d----        948.00  B     ├── Internal
d----         13.73 KB     ├── Nodes
d----        374.92 KB     ├── obj
d----          2.35 KB     ├── Registry
d----         12.53 KB     └── Style
```

#### Get the top N items at each level using `-Top`

```powershell
PS \> Get-PSTree .\src\PSTree -Top 3

   Source: D:\pwsh\PSTree\src\PSTree

Mode            Length Hierarchy
----            ------ ---------
d----          1.15 MB PSTree
d----        732.37 KB ├── bin
d----        732.37 KB │   └── Debug
d----        502.91 KB │       ├── net472
d----        229.46 KB │       └── net8.0
d----        374.92 KB ├── obj
d----        286.14 KB │   ├── Debug
d----        198.00 KB │   │   ├── net8.0
d----         88.14 KB │   │   └── net472
-a---         79.42 KB │   ├── project.assets.json
-a---          4.02 KB │   ├── project.nuget.cache
-----          5.34 KB │   └── [+3 files]
d----         13.73 KB ├── Nodes
-a---          3.61 KB │   ├── TreeBase.cs
-a---          2.55 KB │   ├── TreeRegistryKey.cs
-a---          2.30 KB │   ├── TreeDirectory.cs
-----          5.27 KB │   └── [+6 files]
-----         52.69 KB └── [+8 folders, +1 file]
```

### `Get-PSTreeRegistry`

#### Get the tree-view of `HKCU:\System`

```powershell
PS \> Get-PSTreeRegistry HKCU:\System -Depth 2

   Hive: HKEY_CURRENT_USER\System

Kind         Hierarchy
----         ---------
RegistryKey  System
RegistryKey  ├── GameConfigStore
DWord        │   ├── GameDVR_Enabled
DWord        │   ├── GameDVR_FSEBehaviorMode
Binary       │   ├── Win32_AutoGameModeDefaultProfile
Binary       │   ├── Win32_GameModeRelatedProcesses
DWord        │   ├── GameDVR_HonorUserFSEBehaviorMode
DWord        │   ├── GameDVR_DXGIHonorFSEWindowsCompatible
DWord        │   ├── GameDVR_EFSEFeatureFlags
RegistryKey  │   ├── Parents
RegistryKey  │   └── Children
RegistryKey  └── CurrentControlSet
RegistryKey      ├── Policies
RegistryKey      └── Control
```

#### Filter Out Specific Items from the `HKCU:\System` Tree

```powershell
PS \> Get-PSTreeRegistry HKCU:\System -Depth 2 -Exclude CurrentControlSet, GameDV*

   Hive: HKEY_CURRENT_USER\System

Kind         Hierarchy
----         ---------
RegistryKey  System
RegistryKey  └── GameConfigStore
Binary           ├── Win32_AutoGameModeDefaultProfile
Binary           ├── Win32_GameModeRelatedProcesses
RegistryKey      ├── Parents
RegistryKey      └── Children
```

#### Select GameDVR-Related Values in the `HKCU:\System` Tree

```powershell
PS \> Get-PSTreeRegistry HKCU:\System -Depth 2 -Include GameDVR*

   Hive: HKEY_CURRENT_USER\System

Kind         Hierarchy
----         ---------
RegistryKey  System
RegistryKey  └── GameConfigStore
DWord            ├── GameDVR_Enabled
DWord            ├── GameDVR_FSEBehaviorMode
DWord            ├── GameDVR_HonorUserFSEBehaviorMode
DWord            ├── GameDVR_DXGIHonorFSEWindowsCompatible
DWord            └── GameDVR_EFSEFeatureFlags
```

#### Show only Keys

```powershell
PS \> Get-PSTreeRegistry HKCU:\System -Depth 2 -KeysOnly

   Hive: HKEY_CURRENT_USER\System

Kind         Hierarchy
----         ---------
RegistryKey  System
RegistryKey  ├── GameConfigStore
RegistryKey  │   ├── Parents
RegistryKey  │   └── Children
RegistryKey  └── CurrentControlSet
RegistryKey      ├── Policies
RegistryKey      └── Control
```

#### Get the value of a `TreeRegistryValue` item

```powershell
PS \> $items = Get-PSTreeRegistry HKCU:\Environment\ -Depth 2
PS \> $values = $items | Where-Object { $_ -is [PSTree.Nodes.TreeRegistryValue] }
PS \> $values

   Hive: HKEY_CURRENT_USER\Environment

Kind         Hierarchy
----         ---------
ExpandString ├── Path
ExpandString ├── TEMP
ExpandString └── TMP

PS \> $values[1].GetValue()
C:\Users\User\AppData\Local\Temp
```

## Changelog

- [CHANGELOG.md](CHANGELOG.md)
- [Releases](https://github.com/santisq/PSTree/releases)

## Contributing

Contributions are welcome, if you wish to contribute, fork this repository and submit a pull request with the changes.
