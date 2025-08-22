using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace HorsesForCourses.Blazor.Services;

public class AssignedCourse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly startDate { get; set; }
    public DateOnly endDate { get; set; }
    public IReadOnlyList<Skill> Skills { get; set; } = new List<Skill>();
    public IReadOnlyList<shortTimeslot> ListOfTimeslots { get; set; } = new List<shortTimeslot>();
    public CoachForCourseResponse? assignedCoach { get; set; }

}

public record shortTimeslot(string Day, int beginhour, int endhour);
public record CoachForCourseResponse(int? id, string? name);
