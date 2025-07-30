using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCoursesTests;

public class CoachTest
{
    [Fact]
    public void Coach_Can_Be_Made()
    {
        var slimmeCoach = new Coach("Matthew", "smart@mail.com");

        Assert.IsType<Coach>(slimmeCoach);
    }

    [Fact]
    public void Coach_Can_Add_Competences()
    {
        var slimmeCoach = new Coach("Matthew", "mat@mail.com");
        Skill brainsOverBody = new Competence("karate", 4);
        slimmeCoach.AddCompetence("karate", 4);
        List<Skill> lijstje = slimmeCoach.ListOfCompetences;

        Assert.Contains(brainsOverBody, lijstje);
    }

    [Fact]
    public void Coach_Can_Remove_Competences()
    {
        var slimmeCoach = new Coach("Matthew", "mat@mail.com");
        Skill brainsOverBody = new Competence("karate", 4);
        slimmeCoach.AddCompetence("karate", 4);
        slimmeCoach.RemoveCompetence(brainsOverBody);
        List<Skill> lijstje = slimmeCoach.ListOfCompetences;

        Assert.DoesNotContain(brainsOverBody, lijstje);
    }

}