using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PSTree.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal static class Poly
{
    [Conditional("DEBUG")]
    internal static void Assert([DoesNotReturnIf(false)] bool condition) => Debug.Assert(condition);
}
