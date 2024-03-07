using Dappa.Core.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dappa.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, Guid guid)
    {
        return services.AddDbContext<AppUnitOfWork>(opt => opt.UseInMemoryDatabase(guid.ToString()));
    }
}
