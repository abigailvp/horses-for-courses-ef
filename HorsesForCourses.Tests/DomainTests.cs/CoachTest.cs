using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCoursesTests;

public class CoachTest
{
    [Fact]
    public void Coach_Can_Be_Added()
    {
        var smartCoach = new Coach("Matthew", "smart@mail.com");

        Assert.IsType<Coach>(smartCoach);
        Assert.Equal("Matthew", smartCoach.NameCoach);
        Assert.Equal("smart@mail.com", smartCoach.Email);
    }

    [Fact]
    public void Coach_Can_Add_Skills()
    {
        var smartCoach = new Coach("Matthew", "mat@mail.com");
        Skill brainsOverBody = new Skill("karate");
        smartCoach.AddCompetence("karate");
        List<Skill> list = smartCoach.ListOfCompetences;

        Assert.Contains(brainsOverBody, list);
    }

    [Fact]
    public void Coach_Can_Remove_Skills()
    {
        var smartCoach = new Coach("Matthew", "mat@mail.com");
        Skill brains = new Skill("piano");
        smartCoach.AddCompetence("karate");
        smartCoach.AddCompetence("piano");
        smartCoach.RemoveCompetence("piano");
        List<Skill> list = smartCoach.ListOfCompetences;

        Assert.DoesNotContain(brains, list);
        Assert.Contains(new Skill("karate"), list);
        Assert.Equal(1, list.Count());
    }

    [Fact]
    public void Coach_Can_Add_List_Of_Skills_And_Removes_Old_Skills()
    {
        var smartCoach = new Coach("Matthew", "mat@mail.com");
        smartCoach.AddCompetence("violin");
        List<Skill> skills = new()
        {
            new Skill("piano"),
            new Skill("sowing")
        };

        smartCoach.AddCompetenceList(skills);

        List<Skill> list = smartCoach.ListOfCompetences;

        Assert.DoesNotContain(new Skill("violin"), list);
        Assert.Equal(skills, list);
        Assert.Equal(2, list.Count());
    }

}