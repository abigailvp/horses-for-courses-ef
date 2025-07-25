using HorsesForCourses.Core.HorsesOnTheLoose;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public record Competence
{
    public string Name { get; set; }
    public int Level { get; set; }

    public Competence(string name, int level)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name must be filled in");
        if (level <= 0 || level > 10)
            throw new DomainException("Level must be between 0 and 10");
        Name = name;
        Level = level;
    }
}