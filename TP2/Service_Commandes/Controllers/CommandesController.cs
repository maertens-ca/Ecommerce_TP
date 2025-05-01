using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}