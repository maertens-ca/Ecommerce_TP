namespace Service_Commandes.DTO
{
    public class PanierDto
    {
        public int userId { get; set; }
        public List<ItemPanierDto> itemsPanier { get; set; }
    }
    public class ItemPanierDto
    {
        public int produitId { get; set; }
        public int quantite { get; set; }
    }
}
