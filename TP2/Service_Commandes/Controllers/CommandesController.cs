using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Commandes.DTO;
using Service_Commandes.Models;
using System.Net;
using System.Text.Json;

namespace Service_Commandes.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _options;
        private CommandesDbContext _context;

        public CommandesController()
        {
            _context = new CommandesDbContext();
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [HttpGet("{commandeId}")]
        public async Task<ActionResult<Commande>> GetCommandeById(int commandeId)
        {
            try
            {
                var commande = await _context.Commandes.FindAsync(commandeId);
                if (commande != null)
                {
                    return Ok(commande);
                }
                else
                {
                    return NotFound($"Commande avec ID {commandeId} non trouvée.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<ActionResult<List<Commande>>> GetCommandes()
        {
            try
            {
                var commandes = await _context.Commandes.ToListAsync();
                if (commandes == null || commandes.Count == 0)
                {
                    return NotFound("Aucune commande");
                }
                else
                {
                    return Ok(commandes);
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<Commande>> AddCommande(int userId) // Les commandes sont formées à partir du panier de l'utilisateur
        {
            try
            {
                HttpResponseMessage panierResponse = await _httpClient.GetAsync($"/api/panier/{userId}");
                if (panierResponse.IsSuccessStatusCode) 
                {
                    string contenu = await panierResponse.Content.ReadAsStringAsync();
                    var panier = JsonSerializer.Deserialize<PanierDto>(contenu, new JsonSerializerOptions());

                    if (panier == null || panier.itemsPanier == null || panier.itemsPanier.Count == 0)
                    {
                        return NotFound("Aucun items dans le panier!");
                    }
                    else 
                    {
                        Commande commande = new Commande(DateTime.Now.ToString("yyyy-MM-dd"), userId);
                        foreach (ItemPanierDto item in panier.itemsPanier) 
                        {
                            ItemCommande itemCommande = new ItemCommande(item.produitId, item.quantite);
                            commande.ItemsCommande.Add(itemCommande);
                            
                        } // on sauvegarde uniquement à la fin de la boucle au cas où la commande ne réussit pas à initialiser au complet
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}