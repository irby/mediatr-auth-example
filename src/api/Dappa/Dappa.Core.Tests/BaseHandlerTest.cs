using System.Security.Principal;
using Dappa.Core.Models;
using Dappa.Core.Models.Enums;
using Dappa.Core.Tests.Fakes;
using Dappa.Core.UnitsOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dappa.Core.Tests;

public abstract class BaseHandlerTest
{
    protected BaseHandlerTest()
    { 
        var serviceCollection = new ServiceCollection();
        
        serviceCollection
            .AddRequestValidation()
            .AddMediatR()
            .AddDatabase(Guid.NewGuid());
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        Mediatr = serviceProvider.GetRequiredService<IMediator>();
        Db = serviceProvider.GetRequiredService<AppUnitOfWork>();
        
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var admin = new User
        {
            Role = Role.Admin
        };
        var user = new User
        {
            Role = Role.User
        };
        Db.AddRange(admin, user);
        Db.SaveChanges();

        TestAdmin = admin;
        TestUser = user;
    }

    protected readonly IMediator Mediatr;
    protected readonly AppUnitOfWork Db;

    protected User TestAdmin { get; private set; } = new();
    protected User TestUser { get; private set; } = new();
    protected IIdentity TestIdentity => new FakeIdentity();
}
