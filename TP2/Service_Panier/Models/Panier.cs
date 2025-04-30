namespace Service_Panier.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public ICollection<ItemPanier> ItemsPanier { get; set; } = new List<ItemPanier>();
        public Panier(int userId) { 
            this.userId = userId;
        }
    }



    public class ItemPanier
    {
        public int Id { get; set; }
        public int produitId { get; set; }
        public int quantite { get; set; }
        // clé étrangère pour relation orm
        public int panierId { get; set; }
        public Panier panier { get; set; }

        public ItemPanier(int produitId, int quantite) { 
            this.produitId = produitId;
            this.quantite = quantite;
        }
    }
}
