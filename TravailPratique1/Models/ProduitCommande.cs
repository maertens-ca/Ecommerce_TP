using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravailPratique1.Models
{
    public class ProduitCommande
    {
        [Key]
        public int produitCommandeId { get; set; }
        public int quantité { get; set; } // Quantité de produits dans commande
        public int commandeId { get; set; }
        //[ForeignKey("commandeId")]
        public Commande commande { get; set; }
        public int productId { get; set; }
        //[ForeignKey("productId")]
        public Product product { get; set; }
    }
}
