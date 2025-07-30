namespace HorsesForCourses.WebApi.Factory;


public class AllCoursesResponse
{
    public List<CourseResponse> ListOfCourses { get; set; }
}

public record CourseResponse(int id, string name, string startDate, string endDate, bool hasSchedule, bool hasCoach);




