using AspNetCore.Identity.MongoDbCore.Models;
using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.Models
{
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
        [Required]
        public string Description { get; set; } // Add custom properties like description if needed
    }
}
