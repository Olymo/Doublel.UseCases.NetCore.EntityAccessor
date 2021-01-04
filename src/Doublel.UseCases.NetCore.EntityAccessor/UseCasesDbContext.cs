using Doublel.EntityAccessor;
using Doublel.UseCases;
using Microsoft.EntityFrameworkCore;

namespace Dobulel.UseCases.EntityAccessor
{
    public class UseCasesDbContext : EntityAccessorContext
    {
        public UseCasesDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
    }
}
