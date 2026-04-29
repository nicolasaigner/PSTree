---
external help file: PSTree.dll-Help.xml
Module Name: PSTree
online version: https://github.com/santisq/PSTree/blob/main/docs/en-US/Get-PSTreeStyle.md
schema: 2.0.0
---

# Get-PSTreeStyle

## SYNOPSIS

Retrieves the `TreeStyle` instance used for output rendering.

## SYNTAX

```powershell
Get-PSTreeStyle
    [<CommonParameters>]
```

## DESCRIPTION

The `Get-PSTreeStyle` cmdlet provides access to the `TreeStyle` instance that controls the rendering and customization of output for the `Get-PSTree` and `Get-PSTreeRegistry` cmdlets.

To Customize output rendering, see [__about_TreeStyle__](about_TreeStyle.md).

## EXAMPLES

### Example 1

```powershell
PS \> $style = Get-PSTreeStyle
```

Stores the `TreeStyle` instance in the `$style` variable.

## PARAMETERS

### CommonParameters

This cmdlet supports the common parameters. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### TreeStyle

## NOTES

Modifying the properties of this object (such as colors for files, folders, registry keys, etc.) will immediately affect the
visual output of `Get-PSTree` and `Get-PSTreeRegistry` in the current PowerShell session.

## RELATED LINKS

[__`Get-PSTree`__](Get-PSTree.md)

[__`Get-PSTreeRegistry`__](Get-PSTreeRegistry.md)

[__about_TreeStyle__](about_TreeStyle.md)
