namespace Service_Panier.DTO
{
    // FORMAT À UTILISER LORS DE REQUÊTE POUR ENLEVER UN ITEM DE PANIER
    public class PanierItemRemoveDTO
    {
        public int UserId { get; set; }
        public int produitId { get; set; }
    }
}
