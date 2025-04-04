﻿using Microsoft.AspNetCore.Identity;
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
        public string? GetCurrentUserId() 
        { 
            var Id =  User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Id;
		}
        public IActionResult Index()
        {
            // Simplement afficher liste de tous les products du vendeur
            var Id = GetCurrentUserId();
            if (Id == null) 
            {
                return Unauthorized();
            }
            var vendeur = _DbContext.Users.Find(Id);
            if (vendeur == null) 
            {
                return NotFound();
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
            var products = _DbContext.Products.Where(product => product.vendeur.userId == vendeur.userId).ToList();
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
            var Id = GetCurrentUserId();
            if (Id == null) { }
			return View(product);
        }
    }
}
