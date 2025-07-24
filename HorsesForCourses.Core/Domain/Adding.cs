using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Repo;

namespace HorsesForCourses.Core.Domain;

public class Adding
{
    public string createCoach(CoachDTO dto)
    {
        Coach coach = new Coach(dto.NameCoach, dto.Email)
        {
            CoachId = new Id<Coach>(dto.CoachId),
        };
        AllData.allCoaches.Add(coach);
        return $"Coach {coach.NameCoach} has been added";
    }

    //     public Course createCourse(CourseDTO dto)
    //     {
    //         Course course = new(dto.NameCourse, DateOnly.Parse(dto.EndDateCourse), DateOnly.Parse(dto.StartDateCourse));
    //         AllData.allCourses.Add(course);
    //         return course;
    //     }
    // }

    //   public string CreateCoach(CoachDTO dto)
    //     {
    //         var coach = _adding.createCoach(dto);
    //         return $"Coach {coach.NameCoach} has been added";
}