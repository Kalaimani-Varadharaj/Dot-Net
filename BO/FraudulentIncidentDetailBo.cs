using FraudDetectionRepositoryPatternProject.Models;
using FraudDetectionRepositoryPatternProject.Repository;

namespace FraudDetectionRepositoryPatternProject.BO
{
    public class FraudulentIncidentDetailBo
    {
        private readonly IFraudulentIncidentDetailRepository _fraudulentIncidentRepository = null;

        public FraudulentIncidentDetailBo(IFraudulentIncidentDetailRepository fraudulentIncidentRepository)
        {
            _fraudulentIncidentRepository = fraudulentIncidentRepository;
        }

        public int InsertFraudulentIncident( FraudulentIncidentDetail fraudulentIncident)
        {
            int result;
            try
            {
                if (_fraudulentIncidentRepository != null)
                {
                    result = _fraudulentIncidentRepository.InsertFraudulentIncident(fraudulentIncident);
                }
                else
                {
                    // Log or handle the case where _fraudulentIncidentRepository is null
                    
                    result = -1;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.Message);
                result = -1;
            }

            return result;
        }

        public FraudulentIncidentDetail FindFraudulentIncident(int incidentId)
        {
            FraudulentIncidentDetail incident = _fraudulentIncidentRepository.FindFraudulentIncident(incidentId);
            return incident;
        }

        public IEnumerable<FraudulentIncidentDetail> FindAllFraudulentIncidents()
        {
            IEnumerable<FraudulentIncidentDetail> incidents = _fraudulentIncidentRepository.FindAllFraudulentIncidents();
            return incidents;
        }

        public int UpdateFraudulentIncident(FraudulentIncidentDetail fraudulentIncident)
        {
            int result;
            try
            {
                result = _fraudulentIncidentRepository.UpdateFraudulentIncident(fraudulentIncident);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

            return result;
        }

        public IEnumerable<FraudulentIncidentDetail> FilterFraudulentIncidents()
        {
            IEnumerable<FraudulentIncidentDetail> incidents = _fraudulentIncidentRepository.FilterAllFraudulentIncidents();
            return incidents;
        }

    }
}
