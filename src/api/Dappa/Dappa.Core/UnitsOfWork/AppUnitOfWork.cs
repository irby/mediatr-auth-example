using Dappa.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dappa.Core.UnitsOfWork;

public class AppUnitOfWork : DbContext
{
    public AppUnitOfWork(DbContextOptions<AppUnitOfWork> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}
