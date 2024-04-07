using FraudDetectionRepositoryPatternProject.Models;

namespace FraudDetectionRepositoryPatternProject.Repository
{
    public interface IFraudulentIncidentDetailRepository
    {
        int InsertFraudulentIncident(FraudulentIncidentDetail fraudulentIncident);
        //int InsertFraudulentIncident(int transactionReferenceNumber, FraudulentIncidentDetail fraudulentIncident);
        FraudulentIncidentDetail FindFraudulentIncident(int incidentId);
        IEnumerable<FraudulentIncidentDetail> FindAllFraudulentIncidents();
        int UpdateFraudulentIncident(FraudulentIncidentDetail fraudulentIncident);
        IEnumerable<FraudulentIncidentDetail> FilterAllFraudulentIncidents();

    }
}
