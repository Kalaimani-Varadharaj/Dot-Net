using System.ComponentModel.DataAnnotations;

namespace FraudDetectionRepositoryPatternProject.Models
{
    public class UserLogin
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)] // Optional: Indicates that this property is a password
        public string Password { get; set; }

    }
}
