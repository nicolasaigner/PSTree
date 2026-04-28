$testPath = Split-Path $PSScriptRoot
$isWin = [System.Runtime.InteropServices.RuntimeInformation]::IsOSPlatform(
    [System.Runtime.InteropServices.OSPlatform]::Windows)

$moduleName = (Get-Item ([System.IO.Path]::Combine($PSScriptRoot, '..', 'module', '*.psd1'))).BaseName
$modulePath = [System.IO.Path]::Combine($PSScriptRoot, '..', 'output', $moduleName) | Convert-Path

$testPath, $isWin, $modulePath | Out-Null
Export-ModuleMember -Variable testPath, isWin, modulePath
