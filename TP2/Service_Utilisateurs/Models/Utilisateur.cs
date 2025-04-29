using Newtonsoft.Json;

namespace Service_Utilisateurs.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        public string Role { get; set; }

        public Utilisateur(string username, string nom, string prenom, string email, string role)
        {
            Username = username;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Role = role;
        }
    }
}
