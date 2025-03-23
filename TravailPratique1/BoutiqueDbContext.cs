using Microsoft.EntityFrameworkCore;

namespace TravailPratique1
{
    public class BoutiqueDbContext : DbContext
    {
        //Creer les tables: seance 05 page 18
        public DbSet<Models.Product> Clients { get; set; }
        public DbSet<Models.User> Users { get; set; }



        //Connexion a la base de donnees
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string DatabaseName = "BoutiqueDb";
            dbContextOptionsBuilder.UseSqlServer($"{ConnectionString};Database={DatabaseName};");
        }



    }
}
