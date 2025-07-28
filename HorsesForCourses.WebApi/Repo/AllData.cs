using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi.Factory;
using HorsesForCourses.WebApi.Repo;

namespace HorsesForCourses.WebApi.Repo;

public class AllData
{

    public List<Coach> allCoaches { get; set; } = new();
    public List<Course> allCourses { get; set; } = new();
    public List<Coach> assignedCoaches { get; set; } = new();
    public List<Course> assignedCourses { get; set; } = new();

    public CoachRequest ConvertToCoach(Coach coach)
    => new CoachRequest { CoachId = coach.CoachId.value, NameCoach = coach.NameCoach, Email = coach.Email };

    public CompetentCoachRequest ConvertToCompetentCoach(Coach coach)
    => new CompetentCoachRequest { CoachId = coach.CoachId.value, ListOfCompetences = coach.ListOfCompetences };

    public ScheduledCoachRequest ConvertToScheduledCoach(Coach coach)
    => new ScheduledCoachRequest { CoachId = coach.CoachId.value, CoachTimeslots = coach.AvailableTimeslots };

    public CourseRequest ConvertToCourse(Course course)
    => new CourseRequest { NameCourse = course.NameCourse, StartDateCourse = course.StartDateCourse.ToString(), EndDateCourse = course.EndDateCourse.ToString() };

    public CompetentCourseRequest ConvertToCompetentCourse(Course course)
    => new CompetentCourseRequest { CourseId = course.CourseId.value, ListOfCourseCompetences = course.ListOfCourseCompetences };

    public ScheduledCourseRequest ConvertToScheduledCourse(Course course)
    => new ScheduledCourseRequest { CourseId = course.CourseId.value, CourseTimeslots = course.CourseTimeslots };

    public AssignedCourseRequest ConvertToAssignedCourse(Course course, Coach coach)
   => new AssignedCourseRequest { CourseId = course.CourseId.value, coachId = coach.CoachId.value };
}