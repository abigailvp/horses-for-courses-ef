using System.Reflection.Metadata.Ecma335;
using HorsesForCourses.Core;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCoursesTests;

public class CourseTest
{
    [Fact]
    public void Course_Can_Add_Basic_Course()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));

        Assert.IsType<Course>(tinyCourse);
        Assert.Equal("Cats", tinyCourse.NameCourse);
        Assert.Equal(new DateOnly(2025, 6, 29), tinyCourse.StartDateCourse);
        Assert.Equal(new DateOnly(2025, 7, 28), tinyCourse.EndDateCourse);
    }

    [Fact]
    public void Course_Adds_Timeslots()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
        Timeslot slot = new(9, 11, new DateOnly(2025, 7, 25));
        tinyCourse.AddTimeSlotToCourse(slot);

        List<Timeslot> list = new() { slot };

        Assert.Contains(slot, tinyCourse.CourseTimeslots);
        Assert.Equal(list, tinyCourse.CourseTimeslots);
    }

    [Fact]
    public void Cant_Add_Timeslot_on_sunday()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));

        var fout = Assert.Throws<DomainException>(() => new Timeslot(9, 11, new DateOnly(2025, 7, 20)));
        Assert.Equal("Timeslot can't take place on Saturday or Sunday", fout.Message);

    }

    [Fact]
    public void Cant_Add_Timeslot_before_9()
    {
        var fout = Assert.Throws<DomainException>(() => new Timeslot(7, 11, new DateOnly(2025, 7, 25)));
        Assert.Equal("Timeslot must start between 9 and 17h", fout.Message);
    }

    [Fact]
    public void Cant_Add_Timeslot_with_duration_smaller_Than_1()
    {
        var fout = Assert.Throws<DomainException>(() => new Timeslot(9, 9, new DateOnly(2025, 7, 25)));
        Assert.Equal("Timeslot must have a duration longer than 0 hours", fout.Message);
    }

    [Fact]
    public void Course_Can_Add_And_Remove_Skills()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
        Skill skill = new Skill("piano");
        List<Skill> skills = new(){new Skill("piano"),
            new Skill("sowing")};

        tinyCourse.AddCompetenceList(skills);
        tinyCourse.RemoveCompetence(skill);

        Assert.Contains(new Skill("sowing"), tinyCourse.ListOfCourseSkills);
        Assert.DoesNotContain(skill, tinyCourse.ListOfCourseSkills);

    }


    [Fact]
    public void Course_Can_Add_And_Remove_Timeslots()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
        Timeslot slot = new(9, 11, new DateOnly(2025, 7, 25));
        Timeslot slotThurs = new(9, 11, new DateOnly(2025, 7, 24));
        Timeslot slotWed = new(9, 11, new DateOnly(2025, 7, 23));
        tinyCourse.AddTimeSlotToCourse(slot);
        tinyCourse.AddTimeSlotToCourse(slotThurs);
        tinyCourse.AddTimeSlotToCourse(slotWed);

        tinyCourse.RemoveTimeSlot(slotThurs);

        List<Timeslot> list = new() { slot, slotWed };

        Assert.Contains(slot, tinyCourse.CourseTimeslots);
        Assert.Equal(list, tinyCourse.CourseTimeslots);
    }


    [Fact]
    public void Coach_Can_Add_List_Of_Timeslots()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
        List<Timeslot> slots = new() {
        new(9, 11, new DateOnly(2025, 7, 25)),
       new(9, 11, new DateOnly(2025, 7, 24)),
        new(9, 11, new DateOnly(2025, 7, 23))};

        tinyCourse.AddTimeSlotList(slots);

        Assert.Equal(slots, tinyCourse.CourseTimeslots);
    }

    [Fact(Skip = "notready")]
    public void Course_Confirmed_When_Timeslot_Added()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));
        Timeslot slot = new(9, 11, new DateOnly(2025, 7, 23));
        tinyCourse.AddTimeSlotToCourse(slot);
        tinyCourse.ValidateCourseBasedOnTimeslots(tinyCourse);


    }

    public void Course_Not_Confirmed_When_No_Timeslot_Added()
    {
        var tinyCourse = new Course("Cats", new DateOnly(2025, 6, 29), new DateOnly(2025, 7, 28));

        Assert.Throws<NotReadyException>(() => tinyCourse.ValidateCourseBasedOnTimeslots(tinyCourse));
    }
}