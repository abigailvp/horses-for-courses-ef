namespace HorsesForCourses.Core.WholeValuesAndStuff;

public class Timeslot
{
    public DateOnly DayTimeslot { get; set; }
    public Weekday WeekdayTimeslot { get; set; }
    public int BeginTimeslot { get; set; }
    public int EndTimeslot { get; set; }
    public int DurationTimeslot => EndTimeslot - BeginTimeslot; //methode want is nog niet ingevuld


    private Timeslot(int beginTimeslot, int endTimeslot, DateOnly dayTimeslot)
    {
        BeginTimeslot = beginTimeslot;
        EndTimeslot = endTimeslot;
        DayTimeslot = dayTimeslot;
    }

    public static Timeslot Create(int beginTimeslot, int endTimeslot, DateOnly dayTimeslot)
    {
        if (beginTimeslot < 9 || beginTimeslot > 17)
            throw new ArgumentException("Timeslot must start between 9 and 17h");
        if (endTimeslot < 9 || endTimeslot > 17)
            throw new ArgumentException("Timeslot must end between 9 and 17h");

        if (endTimeslot - beginTimeslot <= 0)
            throw new ArgumentException("Timeslot must have a duration longer than 0 hours");

        if ((int)dayTimeslot.DayOfWeek == 0 || (int)dayTimeslot.DayOfWeek == 6)
            throw new ArgumentException("Timeslot can't take place on Sunday");

        // WeekdayTimeslot = (Weekday)dayTimeslot.DayOfWeek; //omzetten naar enum
        return new Timeslot(beginTimeslot, endTimeslot, dayTimeslot);
    }

}

