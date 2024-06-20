using Application.Abstractions.Data;
using Domain.Animals;
using Domain.Companies;
using Domain.Contracts;
using Domain.Humans;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class ApplicationDbContext : IApplicationDbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Agreement> Agreements { get; set; }
    public DbSet<WorkHour> WorkHours { get; set; }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Human> Humans { get; set; }
    public DbSet<Predator> Predators { get; set; }
}
