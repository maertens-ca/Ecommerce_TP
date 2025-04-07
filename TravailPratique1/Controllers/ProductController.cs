using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravailPratique1.Models;

namespace TravailPratique1.Controllers
{
    public class ProductController : Controller
    {
        private readonly BoutiqueDbContext _DbContext;
        private readonly UserManager<User> _userManager;

        public ProductController(BoutiqueDbContext dbContext,  UserManager<User> userManager) 
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
            // Simplement afficher liste de tous les products du vendeur
            User? vendeur = GetCurrentUser();
            if (vendeur == null) 
            {
                return Unauthorized();
            }
            if (vendeur.GetType() != typeof(Vendeur)) 
            {
                return Unauthorized();
            }
            bool isProduct = _DbContext.Products.Any();
            if (isProduct == false) 
            // Si aucun produit n'existe dans la table
            {
                ViewBag.AucunProduit = "Aucun produit existant!";
                return View();
            }
            // Filtrer produits pour n'afficher que ceux étant possédés par le vendeur actif
            var products = _DbContext.Products.Where(product => product.vendeur.Id == vendeur.Id).ToList();
            // Si le vendeur ne possède aucun produit existant
            if (products.Count == 0)
            {
                ViewBag.AucunProduit = "Vous n'avez aucun produit!";
                return View();
            }
            return View(products);
        }
        [HttpGet]
        public IActionResult Add() // Sans paramètres = On envoie un formulaire d'ajout de produit
        {
            User? user = GetCurrentUser();
            if (user == null)
            {
                return Unauthorized();
            }
            // seuls les vendeurs peuvent ajouter un produit
            else if (user.GetType() != typeof(Vendeur))
            {
                return Unauthorized();
            }
            ViewBag.Action = "Add"; 
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product) // Lorsque le form est rempli
        {
            User? user = GetCurrentUser();
            if (user == null)
            {
                return Unauthorized();
            }
            // seuls les vendeurs peuvent ajouter un produit
            else if (user.GetType() != typeof(Vendeur)) 
            {
                return Unauthorized();
            }
            if (ModelState.IsValid) 
            {
				_DbContext.Add(product); 
                _DbContext.SaveChanges(); 
				return RedirectToAction("Index");
			}
            ViewBag.Action = "Add"; // Réinitialiser l'action du viewbag pour persistance
            return View(product); // si le form n'est pas valide, on retourne dans le view d'ajout
        }
        [HttpGet]
        public IActionResult Edit(int? id) // Layout presque identique au Add, sauf que le formulaire sera rempli avec les informations existantes 
        {
            User? user = GetCurrentUser();
            if (user == null)
            {
                return Unauthorized();
            }
            // seuls les vendeurs peuvent ajouter un produit
            else if (user.GetType() != typeof(Vendeur))
            {
                return Unauthorized();
            }
            if (id == null) 
            { 
                return RedirectToAction("Erreur400", "Home"); 
            }
            var product = _DbContext.Products.Find(id);
            if (product == null) 
            {
                return RedirectToAction("Erreur404", "Home"); 
			}
			ViewBag.Action = "Edit";
			return View(product); 
        }
        [HttpPost]
        public IActionResult Edit(Product product) // Lorsque le form est rempli
        {
            User? user = GetCurrentUser();
            if (user == null)
            {
                return Unauthorized();
            }
            
            else if (user.GetType() != typeof(Vendeur))
            {
                return Unauthorized();
            }
            // Logique de EDIT 
            if (ModelState.IsValid) 
            {
                _DbContext.Update(product);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Action = "Edit"; // Réinitialiser action viewbag pour persistance
            return View(product); // Si le form n'est pas valide, on retourne dans le view de modification
        }
        [HttpPost]
        public IActionResult Delete(int? id) 
        {
            User? user = GetCurrentUser();
            if (user == null)
            {
                return Unauthorized();
            }
            
            else if (user.GetType() != typeof(Vendeur))
            {
                return Unauthorized();
            }
            if (id == null) 
            {
                return RedirectToAction("Erreur400", "Home"); // IL FAUT RETOURNER ERREUR 400
            }
            var product = _DbContext.Products.Find(id);
            if (product == null) 
            {
                return RedirectToAction("Erreur404", "Home"); // IL FAUT RETOURNER ERREUR 404
            }
            _DbContext.Remove(product);
            _DbContext.SaveChanges();
            return RedirectToAction("Index"); // cas normal
        }
        [HttpGet]
        public IActionResult Details(int? id) // Page de produit (Vient avec tous les détails et option d'ajouter au panier)
        {
            if (id == null) 
            {
                return RedirectToAction("Erreur400", "Home"); // Modifier pour que ce soit erreur 400
            }
            var product = _DbContext.Products.Find(id);
			if (product == null)
			{
				return RedirectToAction("Erreur404", "Home"); // IL FAUT RETOURNER ERREUR 404
			}
            User? user = GetCurrentUser();
            if (user == null) 
            {
                return Unauthorized();
            }
			return View(product);
        }
    }
}
