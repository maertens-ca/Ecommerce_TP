using Microsoft.AspNetCore.Mvc;
using Service_Produits.Models;
using System.Net;
using System.Text.Json;

namespace Service_Commandes.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : BaseController
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _options;
        private CommandeDbContext _context;

        public CommandesController()
        {
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [HttpGet{"commandeId"}]
        public async Task<ActionResult<Commande>> GetCommandeById(int commandeId) 
        {
        try
        {
            var produit = _context.Produits.Find(commandeId);
            if (produit != null)
            {
                return Ok(produit);
            }
            else
            {
                return NotFound($"Commande avec ID {commandeId} non trouvée.");
            }
        }
        catch (Exception) { }
        return StatusCode((int)HttpStatusCode.BadRequest);
    }
    }
}