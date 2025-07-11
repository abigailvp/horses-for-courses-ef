namespace HorsesForCourses.Core;

public record Id<T>(Guid value)
{
    public override string ToString() => $"{typeof(T).Name}: {value}";
}