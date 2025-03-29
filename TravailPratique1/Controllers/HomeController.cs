using Microsoft.AspNetCore.Mvc;

namespace TravailPratique1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalogue() 
        { 
            return View(); 
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
