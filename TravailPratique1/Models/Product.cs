using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravailPratique1.Models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string imageUrl { get; set; }
        public double rate { get; set; }
        public int count { get; set; }
        // Relation Plusieurs-à-Plusieurs (Produits -> Commandes) avec table de jonction
        public ICollection<ProduitCommande> produitCommandes { get; set; } = new List<ProduitCommande>();
        public int vendeurId { get; set; } // Clé étrangère
        [ForeignKey("vendeurId")]
        public Vendeur vendeur { get; set; }

    }
}
