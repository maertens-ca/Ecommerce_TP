using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravailPratique1.Models
{
    public class Commande
    {
        [Key]
        public int commandeId { get; set; }
        public string date { get; set; } // formattée en string lors de l'initialisation avec DateTime.Now.ToString("yyyy-MM-dd")
        // Relation Plusieurs-À-Plusieurs (Commandes -> Produits)
        public ICollection<ProduitCommande> produitCommandes { get; set; } = new List<ProduitCommande>();
        public int userId { get; set; } // clé étrangère (Seul client peut faire des commandes)
        [ForeignKey("userId")]
        public Client client { get; set; }
        public int factureId { get; set; } // clé étrangère
        [ForeignKey("factureId")]
        public Facture facture { get; set; }
    }
}
