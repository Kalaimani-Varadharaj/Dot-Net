using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraudDetectionRepositoryPatternProject.Models;

//[MetadataType(typeof(TransactionHistoryAnnotations))]
public partial class TransactionHistory
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionReferenceNumber { get; set; }

    public DateTime? TransactionDateTime { get; set; }

    public decimal Amount { get; set; }

    public long? SenderAccountNumber { get; set; }

    public long? BeneficiaryAccountNumber { get; set; }

    public string SenderName { get; set; } = null!;

    public string BeneficiaryName { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    [Display(Name = "Created At")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Modified At")]
    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<FraudulentIncidentDetail> FraudulentIncidentDetails { get; set; } = new List<FraudulentIncidentDetail>();

    //public TransactionHistory()
    //{
    //    CreatedAt = DateTime.Now;
    //    ModifiedAt = DateTime.Now;
    //}
}
