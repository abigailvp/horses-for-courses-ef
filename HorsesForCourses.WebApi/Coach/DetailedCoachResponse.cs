using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using Microsoft.AspNetCore.SignalR;

namespace HorsesForCourses.WebApi.Factory;


public class DetailedCoachResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Competence> ListOfCompetences { get; set; }
    public List<AssignedCourse> ListOfAssignedCourses { get; set; }

}

public class AssignedCourse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public AssignedCourse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
// { "id": int
// , "name": string
// , "email": string
// , "skills": [string]
// // list of courses this coach is assigned to
// //  mapped to { (course)id, (course)name } object
// , "courses": [{"id":int, "name": string}] 
// }