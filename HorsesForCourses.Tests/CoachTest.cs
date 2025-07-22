using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;

namespace HorsesForCoursesTests;

public class CoachTest
{
    [Fact]
    public void Coach_Can_Be_Made()
    {
        Id<Coach> eersteId = new Id<Coach>(Guid.NewGuid());
        var slimmeCoach = new Coach(eersteId, "Matthew", "smart@mail.com");

        Assert.IsType<Coach>(slimmeCoach);
    }

    [Fact]
    public void Coach_Can_Add_Competences()
    {
        Id<Coach> eersteId = new Id<Coach>(Guid.NewGuid());
        var slimmeCoach = new Coach(eersteId, "Matthew", "mat@mail.com");
        Competence brainsOverBody = new Competence("karate", 4);
        slimmeCoach.AddCompetence("karate", 4);
        List<Competence> lijstje = slimmeCoach.ListOfCompetences;

        Assert.Contains(brainsOverBody, lijstje);
    }

}