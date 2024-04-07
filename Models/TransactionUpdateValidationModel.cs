using System.ComponentModel.DataAnnotations;

namespace FraudDetectionRepositoryPatternProject.Models
{
    public class TransactionUpdateValidationModel : IValidatableObject
    {
        [Required(ErrorMessage = "Amount is required.")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Sender Name is required.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Sender Name should contain alphabets only")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Beneficiary Name is required.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Beneficiary Name should contain alphabets only")]
        public string BeneficiaryName { get; set; }

        [Required(ErrorMessage = "Payment Method is required.")]
        public string PaymentMethod { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Amount.HasValue && Amount <= 0)
            {
                yield return new ValidationResult("Amount should be greater than 0.", new[] { nameof(Amount) });
            }
            // Add more custom validations if needed
        }



    }

}
