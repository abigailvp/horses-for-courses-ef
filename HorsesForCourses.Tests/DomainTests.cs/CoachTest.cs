using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCoursesTests;

public class CoachTest
{
    [Fact(Skip = "notready")]
    public void Coach_Can_Be_Made()
    {
        var slimmeCoach = new Coach("Matthew", "smart@mail.com");

        Assert.IsType<Coach>(slimmeCoach);
    }

    [Fact(Skip = "notready")]
    public void Coach_Can_Add_Competences()
    {
        var slimmeCoach = new Coach("Matthew", "mat@mail.com");
        Skill brainsOverBody = new Skill("karate");
        slimmeCoach.AddCompetence("karate");
        List<Skill> lijstje = slimmeCoach.ListOfCompetences;

        Assert.Contains(brainsOverBody, lijstje);
    }

    [Fact(Skip = "notready")]
    public void Coach_Can_Remove_Competences()
    {
        var slimmeCoach = new Coach("Matthew", "mat@mail.com");
        Skill brainsOverBody = new Skill("karate");
        slimmeCoach.AddCompetence("karate");
        slimmeCoach.RemoveCompetence(brainsOverBody);
        List<Skill> lijstje = slimmeCoach.ListOfCompetences;

        Assert.DoesNotContain(brainsOverBody, lijstje);
    }

}