using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Service_Produits.Controllers
{
    public class ProduitsController : ControllerBase
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _options;

        public ProduitsController()
        {
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
