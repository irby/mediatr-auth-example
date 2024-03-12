using System.Reflection;
using Dappa.Core.Common.Pipeline;
using Dappa.Core.UnitsOfWork;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dappa.Core;

public static class DependencyInjection
{
    private static readonly Assembly _assembly = typeof(DependencyInjection).Assembly;
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(_assembly));
        
        return services;
    }
    
    public static IServiceCollection AddRequestValidation(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddValidatorsFromAssembly(_assembly);
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, Guid guid)
    {
        return services.AddDbContext<AppUnitOfWork>(opt => opt.UseInMemoryDatabase(guid.ToString()));
    }
}
