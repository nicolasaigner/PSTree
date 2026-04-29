using System;

namespace PSTree.Style;

internal readonly struct RenderingSet
{
    private static readonly ValueTuple<string, string> s_fancyGroup = ("├── ", "│   ");
    private static readonly ValueTuple<string, string> s_classicGroup = ("|-- ", "|   ");

    internal static RenderingSet Fancy = new("└── ", true);
    internal static RenderingSet FancyRounded = new("╰── ", true);
    internal static RenderingSet Classic = new("+-- ", false);
    internal static RenderingSet ClassicRounded = new("`-- ", false);

    internal readonly string Corner;
    internal readonly string Space = "    ";
    internal readonly string Branch;
    internal readonly string Vertical;

    private RenderingSet(string corner, bool isFancy)
    {
        Corner = corner;
        (Branch, Vertical) = isFancy ? s_fancyGroup : s_classicGroup;
    }
}
