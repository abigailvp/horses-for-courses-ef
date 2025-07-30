using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using Microsoft.AspNetCore.SignalR;

namespace HorsesForCourses.WebApi.Factory;


public class DetailedCourseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public List<Skill> skills { get; set; }
    public List<MyTimeslot> ListOfTimeslots { get; set; }
    public CoachForCourseResponse coach { get; set; }

}

public record CoachForCourseResponse(int id, string name);
