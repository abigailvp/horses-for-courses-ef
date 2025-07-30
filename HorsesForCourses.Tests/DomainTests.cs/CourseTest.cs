using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCoursesTests;

public class CourseTest
{
    [Fact(Skip = "notready")]
    public void Course_Can_Add_Empty_Course()
    {
        var slimmeCoach = new Coach("Matthew", "smart@mail.com");

        Assert.IsType<Coach>(slimmeCoach);
    }
}