using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Repo;

namespace HorsesForCourses.WebApi.Repo;

public class AllData
{

    public List<Coach> allCoaches { get; set; } = new();
    public List<Course> allCourses { get; set; } = new();
    public List<Coach> assignedCoaches { get; set; } = new();
    public List<Course> assignedCourses { get; set; } = new();

}