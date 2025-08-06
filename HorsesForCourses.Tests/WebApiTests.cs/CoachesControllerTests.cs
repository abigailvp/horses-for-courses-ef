using HorsesForCourses.WebApi.Controllers;
using HorsesForCourses.WebApi.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using HorsesForCourses.WebApi;
using System.Threading.Tasks;
using HorsesForCourses.Core.HorsesOnTheLoose;
using HorsesForCourses.Core.DomainEntities;
using HorsesForCourses.Core.WholeValuesAndStuff;


namespace HorsesForCoursesTests;

public class CoachesControllerTests
{
    [Fact]
    public async Task Coach_Controller_Gets_All_Coaches()
    {
        var connection = new SqliteConnection("Datasource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(connection)
        .Options;

        using (var context = new AppDbContext(options))
        {
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = new AppDbContext(options))
        {
            CoachesController controller = new(context);
            var response = await controller.GetCoaches();
            var okresult = Assert.IsType<OkObjectResult>(response.Result);
            var list = Assert.IsType<ListOfCoachesResponse>(okresult.Value);
            Assert.Empty(list.ListOfCoaches);

        }

        await connection.CloseAsync();

    }

    [Fact]
    public async Task Coach_Controller_Creates_Empty_Coach_And_Finds_It()
    {
        var connection = new SqliteConnection("Datasource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(connection)
        .Options;

        using (var ctx = new AppDbContext(options))
        {
            await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AppDbContext(options))
        {
            CoachesController controller = new(ctx);

            var dto = new CoachRequest
            {
                NameCoach = "Lola",
                Email = "l@example.com",
            };


            var result = await controller.CreateEmptyCoach(dto); //id pas gemaakt als hierin SaveChangesAsync() wordt aangeroepen

            var okResult = Assert.IsType<OkObjectResult>(result.Result); //checkt en returnt type
            var coachid = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, coachid);

        }

        using (var ctx = new AppDbContext(options))
        {
            CoachesController controller = new(ctx);

            var result = await controller.GetCoachById(1);

            var theactionofCoach = Assert.IsType<OkObjectResult>(result.Result);
            var theCoach = Assert.IsType<DetailedCoachResponse>(theactionofCoach.Value);
            Assert.Equal("Lola", theCoach.Name);

        }


        await connection.CloseAsync();

    }

    [Fact]
    public async Task Coach_Controller_Creates_Coach_With_Competences_And_Finds_It()
    {
        var connection = new SqliteConnection("Datasource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(connection)
        .Options;

        using (var ctx = new AppDbContext(options))
        {
            await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AppDbContext(options))
        {
            CoachesController controller = new(ctx);
            var dto = new CoachRequest
            {
                NameCoach = "Lola",
                Email = "l@example.com",
            };

            await controller.CreateEmptyCoach(dto);
            var list = new CompetentCoachRequest
            {
                ListOfSkills = new List<Skill> { new Skill("sowing"), new Skill("driving") }
            };
            await controller.AddCompetencesList(1, list);


        }

        using (var ctx = new AppDbContext(options))
        {
            CoachesController controller = new(ctx);

            var result = await controller.GetCoachById(1);

            var theactionofCoach = Assert.IsType<OkObjectResult>(result.Result);
            var theCoach = Assert.IsType<DetailedCoachResponse>(theactionofCoach.Value);
            Assert.Equal("Lola", theCoach.Name);
            Assert.Equal("sowing", theCoach.ListOfSkills[0].Name);
            Assert.Equal("driving", theCoach.ListOfSkills[1].Name);

        }


        await connection.CloseAsync();

    }


    [Fact]
    public async Task CoachesController_Throws_Exception_When_Adding_Coach_With_Parameters_Missing()
    {
        var cnc = new SqliteConnection("Datasource=:memory:");
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(cnc)
        .Options;

        using (var ctx = new AppDbContext(options))
        {
            await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AppDbContext(options))
        {
            CoachesController controller = new(ctx);

            var dto = new CoachRequest
            {
                NameCoach = "",
                Email = "l@example.com",
            };

            var notWorking = Assert.ThrowsAsync<DomainException>(async () => await controller.CreateEmptyCoach(dto));
            Assert.Equal("Name can't be empty", notWorking.Result.Message);
        }

        await cnc.CloseAsync();

    }

    [Fact]
    public async Task Coach_Controller_Doesnt_Get_NonExisting_Coach()
    {
        var connection = new SqliteConnection("Datasource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(connection)
        .Options;

        using (var ctx = new AppDbContext(options))
        {
            await ctx.Database.EnsureCreatedAsync();

        }

        using (var ctx = new AppDbContext(options))
        {
            CoachesController controller = new(ctx);

            var response = await controller.GetCoachById(3);

            Assert.IsType<NotFoundResult>(response.Result);
        }

        await connection.CloseAsync();
    }



    [Fact]
    public async Task Coach_Controller_Gets_All_Coaches_Coach_By_Id()
    {

        var connection = new SqliteConnection("Datasource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(connection)
        .Options;

        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureCreatedAsync();
        }

        using (var context = new AppDbContext(options))
        {
            CoachesController controller = new(context);

            var notaddedcoach = new CoachRequest { NameCoach = "Lola", Email = "l@example.com" };
            var notaddedothercoach = new CoachRequest { NameCoach = "Lisa", Email = "l@mail.com" };

            await controller.CreateEmptyCoach(notaddedcoach);
            await controller.CreateEmptyCoach(notaddedothercoach);

        }

        using (var context = new AppDbContext(options))
        {
            CoachesController controller = new(context);
            var allCoaches = await controller.GetCoaches();

            var allCoachesResult = Assert.IsType<OkObjectResult>(allCoaches.Result);
            var list = Assert.IsType<ListOfCoachesResponse>(allCoachesResult.Value);
            Assert.Equal(2, list.ListOfCoaches.Count());
            Assert.Equal("Lola", list.ListOfCoaches[0].name);
            Assert.Equal("Lisa", list.ListOfCoaches[1].name);
        }


        await connection.CloseAsync();

    }




}

