using Microsoft.AspNetCore.Mvc;
using Service_Produits.Models;
using System.Net;
using System.Text.Json;

namespace Service_Produits.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _options;
        private ProduitDbContext _context;

        public ProduitsController()
        {
            _context = new ProduitDbContext();
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }



        [HttpGet("{produitId}")]
        public async Task<ActionResult<Produit>> GetProduitById(int produitId)
        {
            try
            {
                var produit = _context.Produits.Find(produitId);
                if (produit != null)
                {
                    return Ok(produit);
                }
                else
                {
                    return NotFound($"Produit avec ID {produitId} non trouvé.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}
