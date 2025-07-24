using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Repo;

namespace HorsesForCourses.WebApi.Repo;

public static class AllData
{
    public static List<Coach> allCoaches { get; set; } = new();
    public static List<Course> allCourses { get; set; } = new();
    public static List<Coach> assignedCoaches { get; set; } = new();

}