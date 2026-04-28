$ErrorActionPreference = 'Stop'

Import-Module ([System.IO.Path]::Combine($PSScriptRoot, 'shared.psm1'))
Import-Module $modulePath

Describe 'Formatting internals' {
    It 'Converts Length to their friendly representation' {
        [PSTree.Internal._FormattingInternals]::GetFormattedLength(1mb) |
            Should -BeExactly '1.00 MB'
    }

    It 'Can get the source of the generated trees' {
        $testPath | Get-PSTree | ForEach-Object {
            [PSTree.Internal._FormattingInternals]::GetSource($_) | Should -BeExactly $testPath
        }

        if ($isWin) {
            Get-PSTreeRegistry HKCU:\ | ForEach-Object {
                [PSTree.Internal._FormattingInternals]::GetSource($_) | Should -BeExactly 'HKEY_CURRENT_USER'
            }
        }
    }
}
