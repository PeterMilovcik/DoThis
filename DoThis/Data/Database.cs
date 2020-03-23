using DoThis.Models;
using Microsoft.EntityFrameworkCore;

namespace DoThis.Data
{
    class Database : DbContext
    {
        public Database()
        {
            Database.Migrate();
        }

        public DbSet<ItemModel> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=items.db");
    }
}
