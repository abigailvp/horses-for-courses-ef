using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;
using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;

// abstract class CrudTestBase<T1, T2>
// {
//     AppDbContext CreateContext(DbContextOptions options);
//     Coach CreateEntity();
//     DbSet<Coach> GetDbSet(AppDbContext context);
//     object[] GetPrimaryKey(Coach entity);
//     Task ModifyEntityAsync(Coach entity);
//     Task AssertUpdatedAsync(Coach entity);

// }

// public class CoachCrudEFTests : CrudTestBase<AppDbContext, Coach>
// {
//     protected override AppDbContext CreateContext(DbContextOptions options) => new AppDbContext(options);

//     protected override Coach CreateEntity()
//         => new Coach("Unit Test", "unit@test.com");

//     protected override DbSet<Coach> GetDbSet(AppDbContext context)
//         => context.Coaches;

//     protected override object[] GetPrimaryKey(Coach entity)
//         => new object[] { entity.CoachId };

//     protected override async Task ModifyEntityAsync(Coach entity)
//     {
//         entity.AddCompetenceList(new List<Skill> { new("unit"), new("test") });
//         await Task.CompletedTask;
//     }

//     protected override async Task AssertUpdatedAsync(Coach entity)
//     {
//         Assert.Contains("unit", entity.ListOfCompetences[0].Name);
//         Assert.Contains("test", entity.ListOfCompetences[1].Name);
//         await Task.CompletedTask;
//     }

// }

