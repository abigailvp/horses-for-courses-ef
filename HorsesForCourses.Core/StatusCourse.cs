namespace HorsesForCourses.Core;

public enum StatusCourse
{
    Created = 1,
    PendingForTimeslots = 2,
    WaitingForTimeslotCheck = 3,
    PendingForCoach = 4,
    Assigned = 5
}