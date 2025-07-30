using HorsesForCourses.Core.HorsesOnTheLoose;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public record Competence
{
    public string Name { get; set; }

    public Competence(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name of competence must be filled in");
        Name = name;
    }
}