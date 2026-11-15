using Jobinja.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Jobinja.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Karjo> Karjos => Set<Karjo>();
    public DbSet<Karfarma> Karfarmas => Set<Karfarma>();
    public DbSet<Agahi> Agahis => Set<Agahi>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}