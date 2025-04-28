namespace Service_Produits.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }



        public Produit(string title, float price, string description, string category, string image)
        {
            this.title = title;
            this.price = price;
            this.description = description;
            this.category = category;
            this.image = image;
        }
    }


}
