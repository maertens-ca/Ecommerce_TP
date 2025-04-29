using Microsoft.AspNetCore.Mvc;
using Service_Utilisateurs.Models;
using System.Net;
using System.Text.Json;

namespace Service_Utilisateurs.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _options;
        private UtilisateurDbContext _context;

        public UtilisateursController()
        {
            _context = new UtilisateurDbContext();
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }



        [HttpGet("{utilisateurId}")]
        public async Task<ActionResult<Models.Utilisateur>> GetUtilisateurById(int utilisateurId)
        {
            try
            {
                var utilisateur = _context.Utilisateurs.Find(utilisateurId);
                if (utilisateur != null)
                {
                    return Ok(utilisateur);
                }
                else
                {
                    return NotFound($"Utilisateur avec ID {utilisateurId} non trouvé.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<ActionResult<Models.Utilisateur>> GetUtilisateurs()
        {
            try
            {
                List<Utilisateur> utilisateurs = _context.Utilisateurs.ToList();
                if (utilisateurs != null)
                {
                    return Ok(utilisateurs);
                }
                else
                {
                    return NotFound($"Aucuns Utilisateurs dans la base de données");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Models.Utilisateur), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Models.Utilisateur>> PostUtilisateur([FromBody] Models.Utilisateur utilisateur)
        {
            try
            {
                if(!(utilisateur.Role.Equals("vendeur") || utilisateur.Role.Equals("client")))
                {
                    return BadRequest("Role doit être vendeur ou client");
                }
                else if (utilisateur != null)
                {
                    _context.Utilisateurs.Add(new Utilisateur(utilisateur.Username, utilisateur.Nom, utilisateur.Prenom, utilisateur.Email, utilisateur.Role));
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetUtilisateurById), new { utilisateurId = utilisateur.Id }, utilisateur);
                }
                else
                {
                    return BadRequest("Utilisateur est null");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        
        [HttpGet("populate")]
        [ProducesResponseType(typeof(Utilisateur), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult PopulateProducts()
        {
            try
            {
                var response = _httpClient.GetAsync("https://fakestoreapi.com/users").Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var utilisateurs = JsonSerializer.Deserialize<List<Utilisateur>>(json, _options);
                    if (utilisateurs != null)
                    {
                        foreach (var utilisateur in utilisateurs)
                        {
                            _context.Utilisateurs.Add(new Utilisateur(utilisateur.Username, utilisateur.Username, utilisateur.Username, utilisateur.Email, "client"));
                        }
                        _context.SaveChanges();
                        return Ok(utilisateurs);
                    }
                    else
                    {
                        return NotFound($"Auncun Utilisateurs trouvé.");
                    }
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpDelete("{utilisateurId}")]
        [ProducesResponseType(typeof(Models.Utilisateur), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Models.Utilisateur>> DeleteUtilisateur(int utilisateurId)
        {
            try
            {
                var utilisateur = _context.Utilisateurs.Find(utilisateurId);
                if (utilisateur != null)
                {
                    _context.Utilisateurs.Remove(utilisateur);
                    _context.SaveChanges();
                    return Ok($"Utilisateur avec ID {utilisateurId} supprimé.");
                }
                else
                {
                    return NotFound($"Utilisateur avec ID {utilisateurId} non trouvé.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPut("{utilisateurId}")]
        [ProducesResponseType(typeof(Models.Utilisateur), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Models.Utilisateur>> PutUtilisateur(int utilisateurId, [FromBody] Models.Utilisateur utilisateur)
        {
            try
            {
                var existingUtilisateur = _context.Utilisateurs.Find(utilisateurId);
                if (!(utilisateur.Role.Equals("vendeur") || utilisateur.Role.Equals("client")))
                {
                    return BadRequest("Role doit être vendeur ou client");
                }
                else if (existingUtilisateur != null)
                {
                    existingUtilisateur.Username = utilisateur.Username;
                    existingUtilisateur.Nom = utilisateur.Nom;
                    existingUtilisateur.Prenom = utilisateur.Prenom;
                    existingUtilisateur.Email = utilisateur.Email;
                    existingUtilisateur.Role = utilisateur.Role;
                    _context.SaveChanges();
                    return Ok(existingUtilisateur);
                }
                else
                {
                    return NotFound($"Utilisateur avec ID {utilisateurId} non trouvé.");
                }
            }
            catch (Exception) { }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }


    }
}
