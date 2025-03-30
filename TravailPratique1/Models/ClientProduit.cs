using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravailPratique1.Models
{
    public class ClientProduit
    {
        [Key]
        public int clientProduitId { get; set; }
        public int quantité { get; set; } // qt. d'un produit dans le panier d'un client
        public int userId { get; set; }
        public Client client { get; set; }
        public int productId { get; set; }
        public Product product { get; set; }
    }
}
