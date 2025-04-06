using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravailPratique1.Models;

namespace TravailPratique1.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoutiqueDbContext _DbContext;
		private readonly UserManager<User> _userManager;

		public HomeController(BoutiqueDbContext dbContext, UserManager<User> userManager)
        {
            _DbContext = dbContext;
            _userManager = userManager;
        }
		public User? GetCurrentUser()
		{
            var Id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (Id == null) 
            {
                return null;
            }
            User? user = _DbContext.Users.Find(Id);
            if (user == null) 
            {
                return null;
            }
            return user;
        }
		public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalogue() 
        {
            bool isProducts = _DbContext.Products.Any();
            if (isProducts != false) 
            {
                return View(_DbContext.Products.ToList());
            }
            ViewBag.AucunProduit = "Aucun produit trouvé!";
            return View();
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
            bool isProducts = _DbContext.Products.Any();
            if (isProducts == true) 
            {
                ViewBag.AucunProduit = "Aucun produit existant!";
                return View();
            }

            var products = _DbContext.Products.Where(product => product.price >= prixMinimum).Where(product => product.price <= prixMaximum);
            if (categorie != null || categorie != "") 
            {
                products = products.Where(product => product.category == categorie);
            }
            if (products.Count() == 0) 
            {
                ViewBag.AucunProduit = "Aucun produit trouvé pour ces filtres!";
                return View();
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
            User? user = GetCurrentUser();
            if (user == null) 
            {
                return Unauthorized();
            }
            if (user.GetType() != typeof(Client)) 
            {
                return Unauthorized();
            }
            Client client = (Client)user;
            if (client.factures.Count() == 0) 
            {
                ViewBag.AucuneCommande = "Aucune commande en lien avec vous!";
                return View();
            }
            return View(client.commandes); // Afficher commandes du client
        }
        public IActionResult Erreur404()
        {
            return View();
        }
        public IActionResult Factures()
        {
            User? user = GetCurrentUser();
            if (user == null)
            {
                return Unauthorized();
            }
            if (user.GetType() == typeof(Vendeur))
            {
                Vendeur vendeur = (Vendeur)user;
                // Retourner liste de factures dédiées au vendeur
                if (vendeur.factures.Count == 0) 
                {
                    ViewBag.AucuneFacture = "Aucune facture dédiée à vous!";
                    return View(); // Sans modèle de facture
                }
                return View(vendeur.factures);
            }
            else // Type client
            {
                Client client = (Client)user;
                // Retourner liste de factures dédiées au client
                if (client.factures.Count == 0)
                {
                    ViewBag.AucuneFacture = "Aucune facture dédiée à vous!";
                    return View(); // Sans modèle de facture
                }
                return View(client.factures);
            }
        }
        public IActionResult Paiement()
        {
            return View();
        }
        public IActionResult Panier()
        {
            User? user = GetCurrentUser();
            if (user == null) 
            {
                return Unauthorized();
            }
			if (user.GetType() != typeof(Client))
			{
				return Unauthorized();
			}
            // Get liste de ClientProduit du client
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
