using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Repo;

namespace HorsesForCourses.WebApi.Repo;

public static class AllData
{
    public static List<Coach> allCoaches { get; set; } = new();
    public static List<Course> allCourses { get; set; } = new();
    public static List<Coach> assignedCoaches { get; set; } = new();

    public static string CreateCoach(CoachDTO dto)
    {
        Coach coach = new Coach(dto.NameCoach, dto.Email)
        {
            CoachId = new Id<Coach>(dto.CoachId)
        };
        AllData.allCoaches.Add(coach);
        return $"Coach {coach.NameCoach} has been added";
    }

    public static string CreateEmptyCourse(CourseDTO dto)
    {
        Course course = new Course(dto.NameCourse, DateOnly.Parse(dto.StartDateCourse), DateOnly.Parse(dto.EndDateCourse));
        //omzetten naar DateOnly
        return $"Course {course.NameCourse} has been added";
    }

}