using Microsoft.AspNetCore.Mvc;
using TravailPratique1.Models;

namespace TravailPratique1.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoutiqueDbContext _DbContext;
        List<Product> testproducts = new List<Product>
            {
                new Product{
                    productId = 1,
                    title = "Chandail",
                    price = 25.20,
                    description = "Beau chandail",
                    category = "Vêtements",
                    imageUrl = "Non"
                },
                new Product{
                    productId = 2,
                    title = "Pantalons",
                    price = 150,
                    description = "Pantalons dégueu",
                    category = "Vêtements",
                    imageUrl = "Non"
                }
            };
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalogue() 
        {
            //List<Product> products = _DbContext.Products.ToList();
            //return View(products);
            return View(testproducts);
        }

        public IActionResult FiltreCatalogue() 
        {
            string? prixMinimumForm = Request.Form["prixMinimum"];
            string? prixMaximumForm = Request.Form["prixMaximum"];
            string? categorie = Request.Form["categorie"];
            double prixMinimum;
            double prixMaximum;
            if (prixMinimumForm == null)
            {
                prixMinimum = 0;
            }
            if (prixMaximumForm == null) 
            {
                prixMaximum = 9999999999;
            }
            prixMinimum = Convert.ToDouble(prixMinimumForm);
            prixMaximum = Convert.ToDouble(prixMaximumForm);
            if (prixMinimum > prixMaximum) 
            {
                prixMaximum = 9999999999;
            }
            var products = _DbContext.Products.Where(product => product.price >= prixMinimum).Where(product => product.price <= prixMaximum);
            if (categorie != null || categorie != "") 
            {
                products = products.Where(product => product.category == categorie);
            }
            return View("Catalogue", products.ToList());
        }

        public IActionResult Inscription()
        {
            return View();
        }
        public IActionResult Authentification()
        {
           
            return View();
        }
        public IActionResult Commandes()
        {
            return View();
        }
        public IActionResult Erreur404()
        {
            return View();
        }
        public IActionResult Factures()
        {
            return View();
        }
        public IActionResult Paiement()
        {
            return View();
        }
        public IActionResult Panier()
        {
            return View();
        }
        public IActionResult Produits()
        {
            return View();
        }
        public IActionResult Profil()
        {
            return View();
        }
    }
}
