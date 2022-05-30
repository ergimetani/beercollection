using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BeerCollectionApi.Models
{
    public class BeerCollectionContext : DbContext
    {
        public BeerCollectionContext(DbContextOptions<BeerCollectionContext> options)
            : base(options)
        {
        }

        public DbSet<Beer> Beers { get; set; } = null!;
    }
}