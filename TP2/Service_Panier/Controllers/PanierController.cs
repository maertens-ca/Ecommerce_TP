using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Panier.DTO;
using Service_Panier.Models;
using System.Net;
using System.Text.Json;

namespace Service_Panier.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PanierController : ControllerBase
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

                    var panier = await _context.Paniers.FirstOrDefaultAsync(panier => panier.userId == userId);
                    if (panier == null) // si pas de panier existant pour le user on lui en crée un
                    {
                        return RedirectToAction(nameof(AddPanierToUser), new { userId = userId });
                    }
                    return Ok(panier);
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
        // Ajouter panier à un user
        public async Task<IActionResult> AddPanierToUser(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/utilisateurs/{userId}");
                if (response.IsSuccessStatusCode) // Simplement checker si utilisateur existe
                {
                    var panier = await _context.Paniers.FirstOrDefaultAsync(panier => panier.userId == userId);
                    if (panier != null) // vérifier qu'il n'existe bien pas de panier pour ce user
                    {
                        return BadRequest("Cet utilisateur a déjà un panier");
                    }
                    Panier nouveauPanier = new Panier(userId);
                    await _context.Paniers.AddAsync(nouveauPanier);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetPanierByUserId), new { userId = nouveauPanier.userId }, nouveauPanier);
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        [HttpPatch("UpdateQuantity/{userId}")]
        public async Task<IActionResult> SetProduitToPanier([FromBody] PanierItemDTO panierItemInfo) 
        {
            try
            {
                // Vérifier que l'utilisateur et le produit existe bien
                HttpResponseMessage produitResponse = await _httpClient.GetAsync($"/api/produits/{panierItemInfo.produitId}");
                HttpResponseMessage userResponse = await _httpClient.GetAsync($"/api/utilisateurs/{panierItemInfo.UserId}");
                if (produitResponse.IsSuccessStatusCode && userResponse.IsSuccessStatusCode)
                {
                    var panier = await _context.Paniers.FirstOrDefaultAsync(panier => panier.userId == panierItemInfo.UserId);
                    if (panier == null) 
                    {
                        return NotFound("L'utilisateur n'a pas de panier!");
                    }
                    var itemPanierExistant = await _context.ItemsPanier.FirstOrDefaultAsync(item => item.produitId ==  panierItemInfo.produitId);
                    if (itemPanierExistant == null) // s'il s'agit d'un nouveau item dans le panier et non d'ajouter à un produit existant
                    {
                        ItemPanier itemPanier = new ItemPanier(panierItemInfo.produitId, int.Max(1, panierItemInfo.quantité)); // pour éviter une quantité de base négative
                        panier.ItemsPanier.Add(itemPanier);
                        await _context.SaveChangesAsync();
                        return Ok(panier);
                    }
                    itemPanierExistant.quantite = int.Max(1, itemPanierExistant.quantite + panierItemInfo.quantité);
                    await _context.SaveChangesAsync();
                    return Ok(panier);
                }
                else 
                {
                    return NotFound("Utilisateur ou produit non trouvé!");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}