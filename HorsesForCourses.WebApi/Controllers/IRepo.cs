using HorsesForCourses.Core.DomainEntities;

public interface IRepo
{
    void AddCoach(Coach coach);
    void RemoveCoach(Coach coach);
    Task<Coach> GetCoachById(int id);
    Task<Coach> GetSpecificCoachById(int id);
    Task<List<Coach>> ListCoaches();

}