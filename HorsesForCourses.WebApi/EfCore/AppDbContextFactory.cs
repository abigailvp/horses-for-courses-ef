using HorsesForCourses.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var padUitBin = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
        var dbPath = Path.Combine(padUitBin, "app.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}"); // Or your connection string

        return new AppDbContext(optionsBuilder.Options);
    }
}