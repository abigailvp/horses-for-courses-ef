using HorsesForCourses.Core.HorsesOnTheLoose;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public record Skill
{
    public string Name { get; set; }

    public Skill(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name of competence must be filled in");
        Name = name;
    }
}