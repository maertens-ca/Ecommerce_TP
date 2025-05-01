namespace Service_Commandes.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public string date { get; set; } // formattée en string lors de l'initialisation avec DateTime.Now.ToString("yyyy-MM-dd")
        public int clientId { get; set; }
        public ICollection<ItemCommande> ItemsCommande { get; set; } = new List<ItemCommande>();

        public Commande(string date, int clientId) 
        { 
            this.date = date;
            this.clientId = clientId;
        }
    }

    public class ItemCommande
    {
        public int Id { get; set; }
        public int produitId { get; set; }
        public int quantité { get; set; }

        // référence clé étrangère
        public int commandeId { get; set; }
        public Commande commande { get; set; }

        public ItemCommande(int produitId, int quantité)
        {
            this.produitId = produitId;
            this.quantité = quantité;
        }
    }
}
