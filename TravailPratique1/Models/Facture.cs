using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravailPratique1.Models
{
    public class Facture
    {
        [Key]
        public int factureId { get; set; }
        public required string date { get; set; } // formattée en string lors de l'initialisation avec DateTime.Now.ToString("yyyy-MM-dd")
        public int clientId { get; set; } 
        [ForeignKey("clientId")]
        public required Client client { get; set; }
        public int vendeurId { get; set; } 
        [ForeignKey("userId")]
        public required Vendeur vendeur { get; set; }
        public int commandeId { get; set; } 
        [ForeignKey("commandeId")]
        public required Commande commande { get; set; }
    }
}
