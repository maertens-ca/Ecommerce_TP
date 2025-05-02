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
                var produit = await _context.Produits.FindAsync(produitId);
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

        [HttpGet]
        public async Task<ActionResult<Produit>> GetProduits()
        {
            try
            {
                List<Produit> produits = _context.Produits.ToList();
                if (produits != null)
                {
                    return Ok(produits);
                }
                else
                {
                    return NotFound($"Auncun Produits trouvé.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Produit), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddProductAsync(string title, float price, string description, string category, string image)
        {
            try
            {
                var produit = new Produit(title, price, description, category, image);

                await _context.Produits.AddAsync(produit);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetProduitById), new { produitId = produit.Id }, produit);
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }


        [HttpDelete("{ProductId}")]
        [ProducesResponseType(typeof(Produit), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductAsync(int ProductId)
        {
            try
            {
                var produit = _context.Produits.Find(ProductId);
                if (produit != null)
                {
                     _context.Produits.Remove(produit);
                    _context.SaveChanges();
                    return Ok($"Produit avec ID {ProductId} supprimé.");
                }
                else
                {
                    return NotFound($"Produit avec ID {ProductId} non trouvé.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);

        }


        [HttpGet("populate")]
        [ProducesResponseType(typeof(Produit), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PopulateProductsAsync()
        {
            try
            {
                var response = _httpClient.GetAsync("https://fakestoreapi.com/products").Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var produits = JsonSerializer.Deserialize<List<Produit>>(json, _options);
                    if (produits != null)
                    {
                        foreach (var produit in produits)
                        {
                             await _context.Produits.AddAsync(new Produit(produit.title, produit.price, produit.category, produit.description, produit.image));
                        }
                        _context.SaveChanges();
                        return Ok(produits);
                    }
                    else
                    {
                        return NotFound($"Auncun Produits trouvé.");
                    }
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPut("{produitId}")]
        public async Task<ActionResult<Produit>> UpdateProduit(int produitId, Produit produit)
        {
            try
            {
                var existingProduit = await _context.Produits.FindAsync(produitId);
                if (existingProduit != null)
                {
                    existingProduit.title = produit.title;
                    existingProduit.price = produit.price;
                    existingProduit.description = produit.description;
                    existingProduit.category = produit.category;
                    existingProduit.image = produit.image;
                    _context.SaveChanges();
                    return Ok(existingProduit);
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
