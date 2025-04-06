using Microsoft.AspNetCore.Identity;

namespace TravailPratique1.Models
{
    // Cette classe sert juste à faire fonctionner la création de BoutiqueDbContext considérant que le type générique soit int
    public class UserRole : IdentityRole<int>
    {   

    }
}
