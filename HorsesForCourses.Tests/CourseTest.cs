using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;

namespace HorsesForCoursesTests;

public class CourseTest
{
    [Fact]
    public void Course_Can_Add_Empty_Course()
    {
        var slimmeCoach = new Coach("Matthew", "smart@mail.com");

        Assert.IsType<Coach>(slimmeCoach);
    }
}