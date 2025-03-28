using Microsoft.AspNetCore.Mvc;
using TravailPratique1.Models;

namespace TravailPratique1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index() 
        {
            // Simplement afficher liste de tous les products
            return View();
        }
        [HttpGet]
        public IActionResult Add() // Sans paramètres = On envoie un formulaire d'ajout de produit
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product) // Lorsque le form est rempli
        {
            // 
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit() // Layout presque identique au Add, sauf que le formulaire sera rempli avec les informations existantes
        { 
            return View(); 
        }
        public IActionResult Edit(Product product) // Lorsque le form est rempli
        {
            // Logique de EDIT (Récupérer le product avec le ID, le supprimer et en créer un nouveau avec même Id)
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int Id) 
        {
            // Écrire logique de delete 
            return RedirectToAction("Index"); // Ramène à la page défaut
        }
    }
}
