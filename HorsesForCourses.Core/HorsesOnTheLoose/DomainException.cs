namespace HorsesForCourses.Core.HorsesOnTheLoose;

public class DomainException : ArgumentException
{
    public DomainException(string boodschap) : base(boodschap) { }
    //dit is de constructor
    //base() roept constructor van ArgumentException aan om boodscahp door te geven
}
