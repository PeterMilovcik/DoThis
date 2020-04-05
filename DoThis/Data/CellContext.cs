using Beeffective.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beeffective.Data
{
    internal class CellContext : DbContext
    {
        public CellContext()
        {
            Database.Migrate();
        }

        public DbSet<CellEntity> Cells { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            options.UseSqlite("Data Source=cells.db");
            options.EnableSensitiveDataLogging();
        }
    }
}