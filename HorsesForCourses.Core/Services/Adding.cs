using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.Core;

namespace HorsesForCourses.Services;

public class Adding : IAdding
{
    public Coach createCoach(CoachDTO dto)
    {
        Coach coach = new Coach(dto.NameCoach, dto.Email)
        {
            CoachId = new Id<Coach>(dto.CoachId),
            ListOfCompetences = dto.ListOfCompetences,
            AvailableTimeslots = dto.AvailableTimeslots
        };
        AllData.allCoaches.Add(coach);
        return coach;

    }

    public Course createCourse(CourseDTO dto)
    {
        Course course = new(dto.NameCourse, DateOnly.Parse(dto.EndDateCourse), DateOnly.Parse(dto.StartDateCourse));
        AllData.allCourses.Add(course);
        return course;
    }
}