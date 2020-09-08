namespace Application.Common.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using Microsoft.EntityFrameworkCore;

    using Domain.Entities;

    public interface IApplicationDbContext
    {
        DbSet<City> Cities { get; set; }

        DbSet<District> Districts { get; set; }

        DbSet<Village> Villages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
