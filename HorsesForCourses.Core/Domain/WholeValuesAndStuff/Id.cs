namespace HorsesForCourses.Core.WholeValuesAndStuff;

public record Id<T>(Guid value)
{
    public override string ToString() => $"{typeof(T).Name}: {value}";
}