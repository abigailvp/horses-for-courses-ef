using HorsesForCourses.Core.WholeValuesAndStuff;

namespace HorsesForCourses.WebApi.Factory;


public class DetailedCoachResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Skill> ListOfSkills { get; set; }
    public List<AssignedCourse> ListOfAssignedCourses { get; set; }

}

public class AssignedCourse
{
    public int IdCourse { get; set; }
    public string NameCourse { get; set; }

    public AssignedCourse(int id, string name)
    {
        IdCourse = id;
        NameCourse = name;
    }
}
