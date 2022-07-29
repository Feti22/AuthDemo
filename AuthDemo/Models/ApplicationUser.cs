using Microsoft.AspNetCore.Identity;

namespace AuthDemo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Vehicule> Vehicules { get; set; } = new HashSet<Vehicule>();
        public ApplicationUser() : base()
        {

        }
    } 
}
