namespace HorsesForCourses.Core.HorsesOnTheLoose;

public class NotReadyException : InvalidOperationException
{
    public NotReadyException(string boodschap) : base(boodschap) { }
    //dit is de constructor
    //base() roept constructor van ArgumentException aan om boodscahp door te geven
}
