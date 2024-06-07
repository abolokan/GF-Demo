using Domain.Companies;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; set; }
    DbSet<Contract> Contracts { get; set; }
    DbSet<Agreement> Agreements { get; set; }
    DbSet<WorkHour> WorkHours { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
