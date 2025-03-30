using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravailPratique1.Models
{
    public class ClientProduit
    {
        [Key]
        public int clientProduitId { get; set; }
        public int quantité { get; set; } // qt. d'un produit dans le panier d'un client
        public int clientId { get; set; }
        [ForeignKey("clientId")]
        public Client client { get; set; }
        public int produitId { get; set; }
        [ForeignKey("productId")]
        public Product product { get; set; }
    }
}
