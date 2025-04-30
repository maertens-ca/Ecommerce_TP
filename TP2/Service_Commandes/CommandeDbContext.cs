using Microsoft.EntityFrameworkCore;

namespace Service_Commandes
{
    public class CommandesDbContext : DbContext
    {
        public DbSet<Models.Commande> Commandes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string database_name = "CommandesDb";
            optionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Commande>().ToTable("Panier");
        }
    }
}
