using Dappa.Core.Models;
using Dappa.Core.Models.Enums;
using Dappa.Core.UnitsOfWork;

namespace Dappa.Api.Extensions;

public static class DbInitializerExtension
{
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var db = serviceProvider.GetService<AppUnitOfWork>()!;
            db.Users.Add(new User
            {
                Id = Guid.Parse("8391955b-6796-4623-8392-a30c69ae45ac"),
                Username = "user",
                Password = "pass",
                Role = Role.User,
            });
            db.Users.Add(new User
            {
                Id = Guid.Parse("8391955b-6796-4623-8392-a30c69ae45ad"),
                Username = "admin",
                Password = "pass",
                Role = Role.Admin,
            });
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return app;
    }
}
