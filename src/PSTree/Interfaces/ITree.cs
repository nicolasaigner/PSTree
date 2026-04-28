namespace PSTree.Interfaces;

public interface ITree
{
    internal bool IsContainer { get; }
    internal string Source { get; }
    public string? Hierarchy { get; internal set; }
    public string Name { get; }
}
