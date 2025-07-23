namespace HorsesForCourses.Core;

public enum StatusCourse
{
    Created = 1,
    PendingForTimeslots = 2,
    WaitingForTimeslotCheck = 3,
    CompetetencesChecked = 4,
    PendingForCompetenceCheck = 5,
    Assigned = 6
}