using HorsesForCourses.Core.HorsesOnTheLoose;

namespace HorsesForCourses.Core.WholeValuesAndStuff;

public class Timeslot
{
    public DateOnly DateTimeslot { get; set; }

    public string Day { get; set; }
    public int BeginTimeslot { get; set; }
    public int EndTimeslot { get; set; }

    public int DurationTimeslot => EndTimeslot - BeginTimeslot;


    public Timeslot(int beginTimeslot, int endTimeslot, DateOnly dateTimeslot)
    {
        if (beginTimeslot < 9 || beginTimeslot > 17)
            throw new DomainException("Timeslot must start between 9 and 17h");
        if (endTimeslot < 9 || endTimeslot > 17)
            throw new DomainException("Timeslot must end between 9 and 17h");

        if (endTimeslot - beginTimeslot <= 0)
            throw new DomainException("Timeslot must have a duration longer than 0 hours");

        if ((int)dateTimeslot.DayOfWeek == 0 || (int)dateTimeslot.DayOfWeek == 6)
            throw new DomainException("Timeslot can't take place on Saturday or Sunday");

        BeginTimeslot = beginTimeslot;
        EndTimeslot = endTimeslot;
        DateTimeslot = dateTimeslot;
        Day = DateTimeslot.DayOfWeek.ToString();
    }

}

