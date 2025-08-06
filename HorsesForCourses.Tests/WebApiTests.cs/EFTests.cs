using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.WebApi;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Tests.EF;

public class CoachPersistancyTests
{
    [Fact]
    public async Task ShouldPersistDataofCoach() //save data
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();
        // zo doen zodat je alleen bij het openen van connectie, data opslaat

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new AppDbContext(options))
        {
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = new AppDbContext(options))
        {
            var coach = new Coach("naam", "em@il");
            coach.AddCompetence("dev");
            context.Coaches.Add(coach);
            await context.SaveChangesAsync();
        }

        using (var context = new AppDbContext(options))
        {
            var coach = await context.Coaches.FindAsync(1);
            Assert.NotNull(coach);
            Assert.Equal("naam", coach!.NameCoach);
            Assert.NotNull(coach.ListOfCompetences);
            Assert.Single(coach.ListOfCompetences);
            Assert.Equal("dev", coach.ListOfCompetences.Single().Name);
        }

        await connection.CloseAsync(); //terug sluiten connectie> data weg
    }

    [Fact]
    public async Task ShouldPersistDataofCourse() //save data
    {
        var connection = new SqliteConnection("DataSource=:memory");
        connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(connection)
        .Options;

        using (new AppDbContext(options))
        { }


    }
}