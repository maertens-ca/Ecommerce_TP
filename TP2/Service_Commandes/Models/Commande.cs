namespace Service_Commandes.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public string date { get; set; } // formatt�e en string lors de l'initialisation avec DateTime.Now.ToString("yyyy-MM-dd")
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
        public int quantit� { get; set; }

        // r�f�rence cl� �trang�re
        public int commandeId { get; set; }
        public Commande commande { get; set; }

        public ItemCommande(int produitId, int quantit�)
        {
            this.produitId = produitId;
            this.quantit� = quantit�;
        }
    }
}
