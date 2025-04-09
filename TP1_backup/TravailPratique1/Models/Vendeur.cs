namespace TravailPratique1.Models
{
    public class Vendeur : User
    {
        public ICollection<Facture> factures { get; set; } = new List<Facture>();
        public ICollection<Product> products { get; set; } = new List<Product>();
    }
}
