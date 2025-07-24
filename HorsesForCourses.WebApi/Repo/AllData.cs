using HorsesForCourses.Core.DomainEntities;

namespace HorsesForCourses.Repo;

public static class AllData
{
    public static List<Coach> allCoaches { get; set; } = new();
    public static List<Course> allCourses { get; set; } = new();
    public static List<Coach> assignedCoaches { get; set; } = new();

}