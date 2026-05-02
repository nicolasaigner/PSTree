# CHANGELOG

## v3.0.0

### Major Changes

- **Major internal refactoring** of the tree node model for better performance, maintainability, and extensibility.
- Moved all tree node classes from the `PSTree` namespace to the new `PSTree.Nodes` namespace.
- Made `Get-PSTreeRegistry` **Windows-only** (cmdlet and alias `pstreereg` are no longer available on non-Windows platforms).
- Significantly improved memory usage and rendering performance.

### New Features

- **Added `-SortBy` parameter** (alias `-sb`) to both `Get-PSTree` and `Get-PSTreeRegistry`.
  - New `FileSystemSortMode` and `RegistrySortMode` enums with multiple sorting strategies (including size-based sorting).
  - Added `TreeComparerFactory` and centralized comparison logic.

- **Added `-Top` parameter** (alias `-t`) to `Get-PSTree`.
  - Limits the number of children displayed per directory.
  - Automatically enables `-RecursiveSize` and defaults to size-based sorting.
  - Throws a clear error if used with incompatible sort modes.
  - Introduces `TreeSummary` nodes to represent truncated items.

- **Added customizable tree rendering styles**:
  - New `RenderingStyle` property on `TreeStyle`.
  - Supported styles: `Fancy` (default), `FancyRounded`, `Classic`, `ClassicRounded`.

- Added `Summary` property to `TreeStyle.FileSystemStyle` for customizing the appearance of `TreeSummary` objects.
- Improved tree rendering logic with `RenderingSet` struct for better control over box-drawing characters.

### Changed

- Refactored tree processing to separate **Data Shaping** (sorting, pruning, truncation) from rendering, improving consistency and performance.
- `TreeBase` is now a true tree structure with parent references and child collections.
- **Registry values now properly populate the `Path` property** (previously empty for `TreeRegistryValue` objects). The path now uses backslash (`\`) separator for both keys and values.
- Improved rendering logic to reduce string allocations (~20% reduction in memory consumption).
- Updated and significantly expanded documentation for `Get-PSTree` (especially `-Top`, `TreeSummary`, and sorting).
- Updated `about_TreeStyle.md` with new rendering style options.

### Breaking Changes

- Namespace change: All tree node types moved from `PSTree.*` to `PSTree.Nodes.*`.  
  **Update your scripts** if you were directly referencing node types.
- `Get-PSTreeRegistry` is no longer available on non-Windows platforms.
- Registry value `Path` property behavior changed (now correctly populated).

### Documentation

- Expanded cmdlet help for `Get-PSTree` with detailed `-Top` parameter documentation and examples.
- Added usage examples for `-SortBy` and `-Top` in README.md.
- Updated `about_TreeStyle.md`.

## v2.2.8

- Adds `LastWriteTime` Property to `TreeRegistryKey` class.

## v2.2.7

- **Ctrl+C Cancellation**: Added support for gracefully canceling operations using <kbd>Ctrl+C</kbd> when the cmdlets are traversing a hierarchy. Previously, the only way to stop the process was by restarting the session.
- **Sort Order for Registry Keys**: Similar to issue #9 but for the `Get-PSTreeRegistry` cmdlet. It now sorts Registry Keys in ascending order.

## v2.2.6

- Adds parameter aliases for `Get-PSTree` and `Get-PSTreeRegistry`, issue #49.

## v2.2.5

- Modifies the `Get-PSTreeRegistry` cmdlet to include registry default values (unnamed values displayed as `(Default)` in regedit), which were previously excluded due to a filter on empty value names.

## v2.2.4

- Optimized code to reduce memory allocations, improving performance for large data sets.
- Added `-Include` and `-Exclude` parameters to the `Get-PSTreeRegistry` cmdlet:
  - `-Exclude`: Filters out registry keys and values based on wildcard patterns.
  - `-Include`: Limits output to registry values matching wildcard patterns.

## v2.2.3

- Introduced the `RegistryStyle` class and added a `Registry` property to the `TreeStyle` class.
- Moved file system style properties from `TreeStyle` to a new `FileSystemStyle` class, with a `FileSystem` property added to `TreeStyle`.
- Added coloring support for `Get-PSTreeRegistry`, allowing customization based on the `Kind` property of `TreeRegistryBase` instances.

## v2.2.2

- Renamed base types by removing the leading `PS` prefix (e.g., `PSTreeFile` → `TreeFile`, `PSTreeRegistryKey` → `TreeRegistryKey`) to streamline naming and improve consistency.
- Added the `Get-PSTreeRegistry` cmdlet, enabling tree-style traversal of the Windows Registry. This Windows-only cmdlet supports parameters like `-Path`, `-LiteralPath`, `-Depth`, `-Force`, and `-KeysOnly` for filtering and controlling output depth. Includes comprehensive Pester tests and documentation updates.

## v2.2.1

- Big code refactoring, this update improves readability and simplicity.
- Updates to `-Include` and `-Exclude` parameters, with this update the patterns are evaluated using the
object's `.Name` property instead of `.FullName`.
- In addition to the above, this update improves how the cmdlet displays trees when `-Include` is used.
Before, the cmdlet would display sub-trees where no file was matched by the include patterns. Now, only trees having files matched by the patterns are displayed.

  ```powershell
  # PSTree v2.2.0
  PS ..\pwsh> Get-PSTree ..\PSTree -Include *.ps1, *.cs -Exclude *tools, *output

    Source: C:\User\PSTree

  Mode            Length Hierarchy
  ----            ------ ---------
  d----         29.57 KB PSTree
  -a---          1.34 KB ├── build.ps1
  d----          0.00  B ├── .github
  d----          4.10 KB │   └── workflows
  d----          4.11 KB ├── .vscode
  d----        229.32 KB ├── assets
  d----          0.00  B ├── docs
  d----         12.55 KB │   └── en-US
  d----         13.63 KB ├── module
  d----          0.00  B ├── src
  d----         11.50 KB │   └── PSTree
  -a---          1.06 KB │       ├── Cache.cs
  -a---          2.65 KB │       ├── CommandWithPathBase.cs
  -a---          2.98 KB │       ├── PSTreeDirectory.cs
  -a---          1.42 KB │       ├── PSTreeFile.cs
  -a---          1.69 KB │       ├── PSTreeFileSystemInfo_T.cs
  -a---        524.00  B │       ├── PSTreeFileSystemInfo.cs
  -a---        404.00  B │       ├── TreeComparer.cs
  d----          0.00  B │       ├── bin
  d----          6.54 KB │       ├── Commands
  d----          3.63 KB │       ├── Extensions
  d----          1.14 KB │       ├── Internal
  d----         16.83 KB │       ├── obj
  d----          9.28 KB │       └── Style
  d----         17.87 KB └── tests
  -a---        765.00  B     ├── FormattingInternals.tests.ps1
  -a---          6.15 KB     ├── GetPSTreeCommand.tests.ps1
  -a---          1.77 KB     ├── PSTreeDirectory.tests.ps1
  -a---        920.00  B     ├── PSTreeFile.tests.ps1
  -a---          2.63 KB     ├── PSTreeFileSystemInfo_T.tests.ps1
  -a---          4.90 KB     └── TreeStyle.tests.ps1

  # PSTree v2.2.1
  PS ..\pwsh> Get-PSTree ..\PSTree -Include *.ps1, *.cs -Exclude tools, output

    Source: C:\User\PSTree

  Mode            Length Hierarchy
  ----            ------ ---------
  d----          1.34 KB PSTree
  -a---          1.34 KB ├── build.ps1
  d----          0.00  B ├── src
  d----         10.70 KB │   └── PSTree
  -a---          1.06 KB │       ├── Cache.cs
  -a---          2.65 KB │       ├── CommandWithPathBase.cs
  -a---          2.98 KB │       ├── PSTreeDirectory.cs
  -a---          1.42 KB │       ├── PSTreeFile.cs
  -a---          1.69 KB │       ├── PSTreeFileSystemInfo_T.cs
  -a---        524.00  B │       ├── PSTreeFileSystemInfo.cs
  -a---        404.00  B │       └── TreeComparer.cs
  d----         17.10 KB └── tests
  -a---        765.00  B     ├── FormattingInternals.tests.ps1
  -a---          6.15 KB     ├── GetPSTreeCommand.tests.ps1
  -a---          1.77 KB     ├── PSTreeDirectory.tests.ps1
  -a---        920.00  B     ├── PSTreeFile.tests.ps1
  -a---          2.63 KB     ├── PSTreeFileSystemInfo_T.tests.ps1
  -a---          4.90 KB     └── TreeStyle.tests.ps1
  ```

## v2.2.0

- Added `TreeStyle` type and `Get-PSTreeStyle` cmdlet for rendering output.
- Added Pester tests for `TreeStyle`.
- Documented changes.

## v2.1.18

- Makes `Depth` property public for `PSTreeFileSystemInfo` instances.
- Makes `GetParents()` method private, absolutely no reason to have it public.
- Added properties `ItemCount` and `TotalItemCount` to `PSTreeDirectory` instances, requested in [**Issue #34**][21].

  ```powershell
  PS \> pstree -Recurse -Force -Directory | Select-Object Hierarchy, Depth, ItemCount, TotalItemCount -First 15

  Hierarchy                  Depth ItemCount TotalItemCount
  ---------                  ----- --------- --------------
  PSTree                         0        15           1476
  ├── .git                       1        13           1078
  │   ├── hooks                  2        13             13
  │   ├── info                   2         1              1
  │   ├── logs                   2         2             24
  │   │   └── refs               3         2             22
  │   │       ├── heads          4         9              9
  │   │       └── remotes        4         1             11
  │   │           └── origin     5        10             10
  │   ├── objects                2       244            995
  │   │   ├── 00                 3         3              3
  │   │   ├── 01                 3         2              2
  │   │   ├── 02                 3         3              3
  │   │   ├── 03                 3         4              4
  │   │   ├── 04                 3         2              2

  PS \> (Get-ChildItem -Force).Count
  15
  PS \> (Get-ChildItem -Force -Recurse).Count
  1476
  PS \> (Get-ChildItem .git -Force).Count
  13
  PS \> (Get-ChildItem .git -Force -Recurse).Count
  1078
  PS \>
  ```

## v2.1.17

- Added method `.GetUnderlyingObject()`. Outputs the underlying `FileSystemInfo` instance.
- Fixes [**Issue #9: Sort by ascending values**][1]:
  - PSTree v2.1.16

    ```powershell
    PS \> pstree -Directory -Depth 2

      Source: D:\...\PSTree

    Mode            Length Hierarchy
    ----            ------ ---------
    d----         25.27 KB PSTree
    d----          3.72 KB ├── tools
    d----         16.96 KB │   ├── ProjectBuilder
    d----          0.00  B │   └── Modules
    d----         13.66 KB ├── tests
    d----          0.00  B ├── src
    d----         13.26 KB │   └── PSTree
    d----        168.69 KB ├── output
    d----         92.50 KB │   ├── TestResults
    d----          0.00  B │   └── PSTree
    d----          6.26 KB ├── module
    d----          0.00  B ├── docs
    d----          7.37 KB │   └── en-US
    d----          4.11 KB ├── .vscode
    d----          0.00  B └── .github
    d----          4.10 KB     └── workflows
    ```

  - PSTree v2.1.17

    ```powershell
    PS \> pstree -Directory -Depth 2

      Source: D:\...\PSTree

    Mode            Length Hierarchy
    ----            ------ ---------
    d----         25.27 KB PSTree
    d----          0.00  B ├── .github
    d----          4.10 KB │   └── workflows
    d----          4.11 KB ├── .vscode
    d----          0.00  B ├── docs
    d----          7.37 KB │   └── en-US
    d----          6.26 KB ├── module
    d----        168.69 KB ├── output
    d----          0.00  B │   ├── PSTree
    d----         92.50 KB │   └── TestResults
    d----          0.00  B ├── src
    d----         13.26 KB │   └── PSTree
    d----         13.66 KB ├── tests
    d----          3.72 KB └── tools
    d----          0.00  B     ├── Modules
    d----         16.96 KB     └── ProjectBuilder
    ```

## v2.1.16

- Added method `.GetFormattedLength()`. Outputs the friendly `.Length` representation of `PSTreeFile` and `PSTreeDirectory` instances.

  ```powershell
  PS \> (Get-PSTree D:\ -RecursiveSize -Depth 0).GetFormattedLength()
  629.59 GB
  ```

## v2.1.15

- Added Parameter `-Include`. Works very similar to `-Exclude`, the patterns are evaluated against the items `.FullName` property, however this parameter targets only files (`FileInfo` instances).

## v2.1.14

- No changes to the cmdlet but a few improvements to the code base:
  - [x] <https://github.com/santisq/PSTree/issues/16> `PSTreeCache` and `PSTreeIndexer` internal classes have been sealed following the recommendations from dotnet/runtime#49944.
  - [x] <https://github.com/santisq/PSTree/issues/17> `Indent` extension method has been changed to use `StringBuilder`.
  - [x] <https://github.com/santisq/PSTree/issues/19> Improved `ConvertToTree` method. Was too complicated and inefficient, there was also no need to use `Regex`.
  - [x] <https://github.com/santisq/PSTree/issues/20> `-Depth` parameter type was changed from `int` to `uint` and the documentation was updated accordingly.

## v2.1.13

- Added `.ToString()` method to `PSTreeFileSystemInfo<T>` instances, the method resolves to the instances `.FullName` property similar to [`FileSystemInfo.ToString` Method][14]. Now it should be possible to pipe `Get-PSTree` output to `Get-Item` and `Get-ChildItem` when needed:

  ```powershell
  Get-PStree -Depth 0 | Get-Item
  ```

- Added `.Refresh()` method to `PSTreeFileSystemInfo<T>`, functionality is the same as [`FileSystemInfo.Refresh` Method][15].
- Reorganizing source files and Pester tests.
- Added more Pester tests.
- Fixed a few documentation typos.

## v2.1.12

- Added `-Path` parameter, now both `-Path` and `-LiteralPath` parameters take `string[]` as input and support pipeline input.
- Added Pester tests, Code Coverage and coverage upload to [codecov.io][16].
- Removed `.Size` Property from `PSTreeFile` and `PSTreeDirectory` instances. The `Size` column has been renamed to `Length` and moved to the left-hand side of the `Hierarchy` column (I know it looks much better on the right-hand side :expressionless: but having it in the left allows for fixed width in the first 2 columns, which in turn brings less formatting issues :man_shrugging:...).

  The default display for this column is available through `[PSTree.Internal._Format]::GetFormattedLength(...)`, for example:

  ```powershell
  Get-PSTree | Select-Object Hierarchy, @{ N='Size'; E={ [PSTree.Internal._Format]::GetFormattedLength($_.Length) }}
  ```

- Added `GroupBy` tag to the default view, now trees are grouped by the source Path omg! Little example:

  ```powershell
  PS \> Get-PSTree .\src\, .\module\ -Exclude *\obj,*\bin

    Source: C:\path\to\PSTree\src

  Mode            Length Hierarchy
  ----            ------ ---------
  d----          0.00  B src
  d----         10.30 KB └── PSTree
  -a---        931.00  B     ├── ExceptionHelpers.cs
  -a---        439.00  B     ├── PSTree.csproj
  -a---          1.06 KB     ├── PSTreeDirectory.cs
  -a---          4.01 KB     ├── PSTreeExtensions.cs
  -a---        517.00  B     ├── PSTreeFile.cs
  -a---        399.00  B     ├── PSTreeFileSystemInfo.cs
  -a---          1.51 KB     ├── PSTreeFileSystemInfo_T.cs
  -a---        897.00  B     ├── PSTreeHelper.cs
  -a---        619.00  B     ├── PSTreeIndexer.cs
  d----          1.13 KB     ├── Internal
  -a---          1.13 KB     │   └── _Format.cs
  d----          5.68 KB     └── Commands
  -a---          5.68 KB         └── GetPSTreeCommand.cs

    Source: C:\path\to\PSTree

  Mode            Length Hierarchy
  ----            ------ ---------
  d----          6.22 KB module
  -a---          1.54 KB ├── PSTree.Format.ps1xml
  -a---          4.67 KB └── PSTree.psd1
  ```

## v2.1.11

- `Get-PSTree` is now a binary cmdlet. Functionality remains the same. Big thanks to [SeeminglyScience][17] and [jborean93][18] for all their help!
- Added `-Exclude` parameter to the cmdlet. The parameter accepts wildcards and patterns are matched with the object's `.FullName` property. For more details checkout [cmdlet docs][19].

## v2.1.10

- Fixed a bug that made `Get-PSTree` use `-Recurse` by default.
- Added [ETS properties](./PSTree/PSTree.Types.ps1xml) to `PSTreeDirectory` and `PSTreeFile` instances that would make exporting the output easier.

## v2.1.9

- **PSTree Module** is now published to the [PowerShell Gallery][20]!
- Introducing `-RecursiveSize` switch parameter to `Get-PSTree`. By default, `Get-PSTree` only displays the size of folders **based on the sum of the files length in each Directory**.
This parameter allows to calculate the recursive size of folders in the hierarchy, similar to how explorer does it. **It's important to note that this is a more expensive operation**, in order to calculate the recursive size, all folders in the hierarchy need to be traversed.

```powershell
PS \> pstree -Directory -Depth 2

Mode  Hierarchy          Size
----  ---------          ----
d---- PSTree          9.51 Kb
d---- └── PSTree      4.83 Kb
d----     ├── public   4.8 Kb
d----     ├── private 0 Bytes
d----     └── Format  1.83 Kb

PS \> pstree -Directory -Depth 2 -RecursiveSize

Mode  Hierarchy            Size
----  ---------            ----
d---- PSTree          180.38 Kb
d---- └── PSTree       14.75 Kb
d----     ├── public     4.8 Kb
d----     ├── private   3.29 Kb
d----     └── Format    1.83 Kb
```

## v2.1.8

- Added [format view][12] for the Module - [`PSTree.Format.ps1xml`][13].
- The module now uses [`EnumerateFileSystemInfos()`][11] instance method.
- Improved error handling (a lot).
- `-Files` parameter has been replaced with `-Directory` parameter, now the module displays files by default.
- `-Deep` parameter has been replaced with `-Recurse` parameter, same functionality.
- `PSTreeDirectory` and `PSTreeFile` instances now only include 2 visible properties, `Hierarchy` and `Length`, the rest is done with format view.

```powershell
PS \> pstree -Recurse

Mode  Hierarchy                             Size
----  ---------                             ----
d---- PSTree                            10.21 Kb
-a--- ├── LICENSE                        1.07 Kb
-a--- ├── README.md                      9.15 Kb
d---- └── PSTree                         4.83 Kb
-a---     ├── PSTree.psd1                4.57 Kb
-a---     ├── PSTree.psm1              270 Bytes
d----     ├── public                      4.8 Kb
-a---     │   └── Get-PSTree.ps1          4.8 Kb
d----     ├── private                    0 Bytes
d----     │   └── classes                3.29 Kb
-a---     │       └── classes.ps1        3.29 Kb
d----     └── Format                     1.83 Kb
-a---         └── PSTree.Format.ps1xml   1.83 Kb
```

## v2.1.7 (and before)

- Lots of code improvements have been done to the Module and improved error handling. Now uses the [`GetDirectories()`][10] and [`GetFiles()`][9] methods from [`System.IO.DirectoryInfo`][8]. Each `PSTreeDirectory` instance now holds an instance of `DirectoryInfo`. [`System.Collections.Stack`][5] has been changed for [`System.Collections.Generic.Stack<T>`][7].

- **PSTree Module** now uses [`System.Collections.Stack`][5] instead of recursion, performance should be much better now and functionality remains the same. Special thanks to [IISResetMe][6].

- **PSTree Module** now has it's own classes, functionality remains the same however a lot has been improved.
- Recursion is now done using the static methods [`[System.IO.Directory]::GetDirectories()`][2] and [`[System.IO.Directory]::GetFiles()`][3] instead of [`Get-ChildItem`][4].

- `-Files` switch has been added to the Module, now you can display files in the hierarchy tree if desired.
- `Type` property has been added to the output object and is now part of the _Default MemberSet_.

[1]: https://github.com/santisq/PSTree/issues/9
[2]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getdirectories
[3]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles
[4]: https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/get-childitem
[5]: https://docs.microsoft.com/en-us/dotnet/api/system.collections.stack
[6]: https://github.com/IISResetMe
[7]: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1
[8]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo
[9]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getfiles#system-io-directoryinfo-getfiles
[10]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getdirectories#system-io-directoryinfo-getdirectories
[11]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.enumeratefilesysteminfos#system-io-directoryinfo-enumeratefilesysteminfos
[12]: https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_format.ps1xml?view=powershell-7.2&viewFallbackFrom=powershell-6
[13]: PSTree/PSTree.Format.ps1xml
[14]: https://learn.microsoft.com/en-us/dotnet/api/system.io.filesysteminfo.tostring#system-io-filesysteminfo-tostring
[15]: https://learn.microsoft.com/en-us/dotnet/api/system.io.filesysteminfo.refresh#system-io-filesysteminfo-refresh
[16]: https://app.codecov.io/gh/santisq/PSTree
[17]: https://github.com/SeeminglyScience/
[18]: https://github.com/jborean93/
[19]: /docs/en-US/Get-PSTree.md
[20]: https://www.powershellgallery.com/
[21]: https://github.com/santisq/PSTree/issues/34
