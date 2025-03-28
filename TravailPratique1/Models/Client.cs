namespace TravailPratique1.Models
{
    public class Client : User
    {
        public ICollection<Commande> commandes { get; set; } = new List<Commande>();
        public ICollection<Facture> factures { get; set; } = new List<Facture>();
    }
}
