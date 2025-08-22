using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Net.Http.Json;

namespace HorsesForCourses.Blazor.Services;

public class AssignedCoach
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IReadOnlyList<Skill> ListOfCompetences { get; set; } = new List<Skill>(); //zelfde namen geven als dto's in backend
    public IReadOnlyList<Course> ListOfCoursesAssignedTo { get; set; } = new List<Course>();
}

public record Skill(string Name);
public record Course(int Id, string Name);


