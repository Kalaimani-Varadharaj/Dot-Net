using FraudDetectionRepositoryPatternProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FraudDetectionRepositoryPatternProject.Repository
{
    public class FraudulentIncidentDetailRepository : IFraudulentIncidentDetailRepository
    {
        private readonly FraudRiskManagementContext _context;

        //Initializing the FraudRiskManagementContext instance which is received as a arugument
        public FraudulentIncidentDetailRepository(FraudRiskManagementContext context)
        {
            _context = context;
        }

        //intializing the FraudRiskManagementContext instance
        public FraudulentIncidentDetailRepository()
        {
            _context = new FraudRiskManagementContext();
        }

        public int InsertFraudulentIncident(FraudulentIncidentDetail fraudulentIncident)
        {
            _context.FraudulentIncidentDetails.Add(fraudulentIncident);
            _context.SaveChanges();
            return fraudulentIncident.IncidentNumber;
        }

        //public int InsertFraudulentIncident(int transactionReferenceNumber, FraudulentIncidentDetail fraudulentIncident)
        //{
        //    fraudulentIncident.TransactionReferenceNumber = transactionReferenceNumber;
        //    _context.FraudulentIncidentDetails.Add(fraudulentIncident);
        //    _context.SaveChanges();
        //    return fraudulentIncident.IncidentNumber;
        //}

        public FraudulentIncidentDetail FindFraudulentIncident(int incidentId)
        {
            // Assuming _context is your DbContext instance
            return _context.FraudulentIncidentDetails
                .Include(f => f.TransactionReferenceNumberNavigation)  // Include the related data
                .FirstOrDefault(f => f.IncidentNumber == incidentId);
        }


        public IEnumerable<FraudulentIncidentDetail> FindAllFraudulentIncidents()
        {
            return _context.FraudulentIncidentDetails.ToList();
        }

        public int UpdateFraudulentIncident(FraudulentIncidentDetail fraudulentIncident)
        {
            _context.Entry(fraudulentIncident).State = EntityState.Modified;
            _context.SaveChanges();
            return fraudulentIncident.IncidentNumber;
        }

        public IEnumerable<FraudulentIncidentDetail> FilterAllFraudulentIncidents()
        {
            IEnumerable<FraudulentIncidentDetail> incidents = (from incident in _context.FraudulentIncidentDetails
                                                            where incident.IncidentStatus == "Opened" && incident.FraudulentType == "Debit Card Fraud"
                                                            select incident);
            return incidents;
        }
    }
}
