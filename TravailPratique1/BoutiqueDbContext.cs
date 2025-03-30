using Microsoft.EntityFrameworkCore;

namespace TravailPratique1
{
    public class BoutiqueDbContext : DbContext
    {
        //Creer les tables: seance 05 page 18
        public DbSet<Models.Product> Clients { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Commande> Commandes { get; set; }
        public DbSet<Models.Facture> Factures { get; set; }
        public DbSet<Models.ProduitCommande> ProduitCommandes { get; set; }
        public DbSet<Models.ClientProduit> ClientProduits { get; set; }
        public DbSet<Models.Vendeur> Vendeurs { get; set; }
        public DbSet<Models.Product> Products { get; set; }

        //Connexion a la base de donnees
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string DatabaseName = "BoutiqueDb";
            dbContextOptionsBuilder.UseSqlServer($"{ConnectionString};Database={DatabaseName};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Annuler le comportement de supprimer par cascade pour les classes suivantes:
            // ClientProduit (Client et product)
            modelBuilder.Entity<Models.ClientProduit>()
                .HasOne(cp => cp.client)
                .WithMany(client => client.clientProduits)
                .HasForeignKey(cp => cp.userId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Models.ClientProduit>()
                .HasOne(cp => cp.product)
                .WithMany(p => p.clientProduits)
                .HasForeignKey(cp => cp.productId)
                .OnDelete(DeleteBehavior.Restrict);

            //Facture (Client, Vendeur et Commande)
            modelBuilder.Entity<Models.Facture>()
                .HasOne(f => f.client)
                .WithMany(client => client.factures)
                .HasForeignKey(f => f.clientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Facture>()
                .HasOne(f => f.vendeur)
                .WithMany(vendeur => vendeur.factures)
                .HasForeignKey(f => f.vendeurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Facture>()
                .HasOne(f => f.commande)
                .WithMany()
                .HasForeignKey(f => f.commandeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            // ProduitCommande (Product et Commande)
            modelBuilder.Entity<Models.ProduitCommande>()
                .HasOne(pc => pc.product)
                .WithMany(prod => prod.produitCommandes)
                .HasForeignKey(pc => pc.productId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.ProduitCommande>()
                .HasOne(pc => pc.commande)
                .WithMany(com => com.produitCommandes)
                .HasForeignKey(pc => pc.commandeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Commande (Client et facture)
            /*
            modelBuilder.Entity<Models.Commande>()
                .HasOne(c => c.client)
                .WithMany()
                .HasForeignKey(c =>c.clientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Commande>()
                .HasOne(c => c.facture)
                .WithMany()
                .HasForeignKey(c => c.factureId)
                .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
