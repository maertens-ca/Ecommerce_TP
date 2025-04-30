using Microsoft.AspNetCore.Mvc;
using Service_Panier.Models;
using System.Net;
using System.Text.Json;

namespace Service_Panier.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PanierController : BaseController
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _options;
        private PanierDbContext _context;

        public PanierController()
        {
            _context = new PanierDbContext();
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Panier>> GetPanierByUserId(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/utilisateurs/{userId}");
                if (response.IsSuccessStatusCode) // Simplement checker si utilisateur existe
                {
                    // Recuperer info sur user si son role est client ou vendeur
                    //string responseContent = await response.Content.ReadAsStringAsync();

                    var panier = await _context.Panier.Where(panier => panier.UserId == userId);
                    if (panier == null) // si pas de panier existant pour le user on lui en crée un
                    {

                    }
                }
                else
                {
                    return NotFound($"Utilisateur avec Id {userId} non trouvé.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddPanier(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/utilisateurs/{userId}");
                if (response.IsSuccessStatusCode) // Simplement checker si utilisateur existe
                {
                    var panier = await _context.Panier.Where(panier => panier.UserId == userId);
                    if (panier != null) // vérifier qu'il n'existe bien pas de panier pour ce user
                    {
                        return BadRequest("Cet utilisateur a déjà un panier");
                    }
                    Panier nouveauPanier = new Panier(userId);
                    _context.Panier.Add(nouveauPanier);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetPanierByUserId), new { userId = nouveauPanier.userId }, nouveauPanier);
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}