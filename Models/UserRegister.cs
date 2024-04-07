using System.ComponentModel.DataAnnotations;

namespace FraudDetectionRepositoryPatternProject.Models
{
    public class UserRegister
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)] // Optional: Indicates that this property is a password
        public string Password { get; set; }

        [Required]
        [Compare("Password")] // Optional: Compares with the 'Password' property for confirmation
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
