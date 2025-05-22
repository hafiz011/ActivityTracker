using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
