using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace HorsesForCourses.Tests;

internal class CustomWebAppFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        // var factory = new CustomWebAppFactory();
        // var client = factory.CreateClient();
        var projectDir = Directory.GetCurrentDirectory();
        builder.UseContentRoot(projectDir);
        return base.CreateHost(builder);
    }
}