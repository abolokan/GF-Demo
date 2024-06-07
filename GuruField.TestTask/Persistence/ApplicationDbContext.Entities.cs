using Application.Abstractions.Data;
using Domain.Companies;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class ApplicationDbContext : IApplicationDbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Agreement> Agreements { get; set; }
    public DbSet<WorkHour> WorkHours { get; set; }

}
