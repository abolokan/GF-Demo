using Domain.Animals;
using Domain.Companies;
using Domain.Contracts;
using Domain.Humans;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; set; }
    DbSet<Contract> Contracts { get; set; }
    DbSet<Agreement> Agreements { get; set; }
    DbSet<WorkHour> WorkHours { get; set; }


    DbSet<Animal> Animals { get; set; }
    DbSet<Human> Humans { get; set; }
    DbSet<Predator> Predators { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
