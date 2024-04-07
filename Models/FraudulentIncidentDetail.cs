using System;
using System.Collections.Generic;

namespace FraudDetectionRepositoryPatternProject.Models;

public partial class FraudulentIncidentDetail
{
    public int IncidentNumber { get; set; }

    public int? TransactionReferenceNumber { get; set; }

    public string IncidentStatus { get; set; } = null!;

    public string FraudulentType { get; set; } = null!;

    public string Comments { get; set; } = null!;


    public virtual TransactionHistory? TransactionReferenceNumberNavigation { get; set; }
}
