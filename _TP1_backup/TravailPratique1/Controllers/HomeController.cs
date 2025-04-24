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
            ViewBag.Titre = "Commandes";
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
                ViewBag.AucunItem = "Aucune commande en lien avec vous!";
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
            ViewBag.Titre = "Factures";
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

                    ViewBag.AucunItem = "Aucune facture dédiée à vous!";
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
                    ViewBag.AucunItem = "Aucune facture dédiée à vous!";
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
            Client client = (Client)user;
            if (client.clientProduits.Count == 0)
            {
                ViewBag.AucunItem = "Aucun produit dans le panier!";
                return View();
            }
            return View(client.clientProduits);
        }
        public IActionResult AjouterAuPanier(int id, int quantity, bool ajax = false)
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
            Product? product = _DbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                foreach (ClientProduit cp in client.clientProduits)
                {
                    // Si le produit est déjà dans le panier du client
                    if (cp.product.productId == product.productId)
                    {
                        _DbContext.ClientProduits.Find(cp.clientProduitId).quantité += quantity;
                        _DbContext.SaveChanges();
                        if (ajax == true)
                        {
                            return Json(new { success = true });
                        }
                        return RedirectToAction("Panier");
                    }

                } // S'il existe pas on crée un nouveau item pour le panier
                ClientProduit nouveauItem = new ClientProduit
                {
                    quantité = 1,
                    Id = client.Id,
                    client = client,
                    productId = product.productId,
                    product = product
                };
                _DbContext.ClientProduits.Add(nouveauItem);
                _DbContext.SaveChanges();

            }
            if (ajax == true)
            {
                return Json(new { success = true });
            }
            return RedirectToAction("Panier");
        }
        public IActionResult EnleverDuPanier(int id) 
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
            Product? product = _DbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            foreach (ClientProduit cp in client.clientProduits)
            {
                
                if (cp.product.productId == product.productId)
                {
                    _DbContext.ClientProduits.Remove(cp);
                    _DbContext.SaveChanges();
                    return RedirectToAction("Panier");
                }
            }
            return NotFound();
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
