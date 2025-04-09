using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravailPratique1.Models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }
        [Required(ErrorMessage ="Champ requis!")]
        public string title { get; set; }
		[Required(ErrorMessage = "Champ requis!")]
		public double price { get; set; }
		[Required(ErrorMessage = "Champ requis!")]
		public string description { get; set; }
		[Required(ErrorMessage = "Champ requis!")]
		public string category { get; set; }
		[Required(ErrorMessage = "Champ requis!")]
		public string imageUrl { get; set; }
        // Relation Plusieurs-à-Plusieurs (Produits -> Commandes) avec table de jonction
        public ICollection<ProduitCommande> produitCommandes { get; set; } = new List<ProduitCommande>();
        // Relation Plusieurs-à-Plusieurs (Produits -> Client) avec table de jonction (utilisée pour populer panier de client)
        public ICollection<ClientProduit> clientProduits { get; set; } = new List<ClientProduit>();
        public int vendeurId { get; set; } // Clé étrangère
        [ForeignKey("vendeurId")]
        public Vendeur vendeur { get; set; }
    }
}
