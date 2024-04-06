using System.Reflection;

namespace ResultExtensions.Common;

public abstract class Enumeration : IEquatable<Enumeration>
{
    protected Enumeration(string name) => Name = name;

    public string Name { get; }

    public static bool operator ==(Enumeration? left, Enumeration? right) => Equals(left, right);

    public static bool operator !=(Enumeration? left, Enumeration? right) => !Equals(left, right);
    
    public static IEnumerable<T> List<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public
                            | BindingFlags.Static
                            | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null)).Cast<T>();
    
    public bool Equals(Enumeration? other) => 
        other is not null && Name.Equals(other.Name, StringComparison.Ordinal);

    public override bool Equals(object? obj) => 
        obj is Enumeration other && Equals(other);

    public override int GetHashCode() => Name.GetHashCode();
}