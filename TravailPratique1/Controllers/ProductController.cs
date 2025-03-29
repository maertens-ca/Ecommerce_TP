using Microsoft.AspNetCore.Mvc;
using TravailPratique1.Models;

namespace TravailPratique1.Controllers
{
    public class ProductController : Controller
    {
        private readonly BoutiqueDbContext _DbContext;
        public IActionResult Index() 
        {
            // Simplement afficher liste de tous les products
            return View();
        }
        [HttpGet]
        public IActionResult Add() // Sans paramètres = On envoie un formulaire d'ajout de produit
        {
            ViewBag.Action = "Add"; 
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product) // Lorsque le form est rempli
        {
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
            if (id == null) 
            { 
                return RedirectToAction("Index"); // DOIT RETOURNER ERREUR 400 
            }
            var product = _DbContext.Products.Find(id);
            if (product == null) 
            {
                return RedirectToAction("Index"); // DOIT RETOURNER ERREUR 404
			}
			ViewBag.Action = "Edit";
			return View(product); 
        }
        [HttpPost]
        public IActionResult Edit(Product product) // Lorsque le form est rempli
        {
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
            if (id == null) 
            {
                return RedirectToAction("Index"); // IL FAUT RETOURNER ERREUR 400
            }
            var product = _DbContext.Products.Find(id);
            if (product == null) 
            {
                return RedirectToAction("Index"); // IL FAUT RETOURNER ERREUR 404
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
                return RedirectToAction("Index"); // Modifier pour que ce soit erreur 400
            }
            var product = _DbContext.Products.Find(id);
			if (product == null)
			{
				return RedirectToAction("Index"); // IL FAUT RETOURNER ERREUR 404
			}
			return View(product);
        }
    }
}
