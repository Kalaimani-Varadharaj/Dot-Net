using FraudDetectionRepositoryPatternProject.BO;
using FraudDetectionRepositoryPatternProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Cors;
//using EnableCorsAttribute = Microsoft.AspNetCore.Cors.EnableCorsAttribute;

namespace FraudDetectionRepositoryPatternProject.Controllers
{
    //[Microsoft.AspNetCore.Cors.EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FraudulentIncidentDetailApiController : ControllerBase
    {
        private FraudulentIncidentDetailBo _fraudulentIncidentBo;
        private TransactionHistoryBo _historyBo;

        public FraudulentIncidentDetailApiController(FraudulentIncidentDetailBo fraudulentIncidentBo, TransactionHistoryBo historyBo)
        {
            _fraudulentIncidentBo = fraudulentIncidentBo;
            _historyBo = historyBo;
        }

        [Route("AddFraudulentIncident")]
        [HttpPost]
        public ActionResult<IEnumerable<FraudulentIncidentDetail>> AddIncident([FromBody] FraudulentIncidentDetail incidentDetail)
        {

            //if (!ModelState.IsValid)
            //{
            //    // If validation fails, return BadRequest with validation errors
            //    var validationErrors = ModelState.Values
            //        .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            //        .ToList();

            //    return BadRequest(new { Status = "Error", Message = "Validation failed", Errors = validationErrors });
            //}

            Console.WriteLine($"Received IncidentNumber: {incidentDetail.IncidentNumber}");
            Console.WriteLine($"Received IncidentStatus: {incidentDetail.IncidentStatus}");
            // ... other properties

            try
            {
                    int incidentId = _fraudulentIncidentBo.InsertFraudulentIncident(incidentDetail);

                    if (incidentId > 0)
                    {
                        return Ok(new { Status = "Success", Message = "Fraudulent Incident Details added successfully", IncidentID = incidentId });
                    }
                    else
                    {
                        return BadRequest(new { Status = "Error", Message = "Failed to add Fraudulent Incident details" });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
                }
            
        }


        [Route("GetIncident/{incidentId}")]
        [HttpGet]
        // https://localhost:7270/api/FraudulentIncidentDetailApi/GetIncident/123
        public ActionResult FetchIncident(int incidentId)
        {
            try
            {
                var incident = _fraudulentIncidentBo.FindFraudulentIncident(incidentId);

                if (incident != null)
                {
                    var result = new
                    {
                        
                        TransactionReferenceNumber = incident.TransactionReferenceNumber,
                        IncidentStatus = incident.IncidentStatus,
                        FraudulentType = incident.FraudulentType,
                        Comments = incident.Comments
                    };

                    return Ok(result);
                }
                else
                {
                    return NotFound(new { Status = "Error", Message = "Fraudulent Incident not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
            }
        }



        [Route("GetAllIncidents")]
        [HttpGet]
        //https://localhost:7270/api/FraudulentIncidentDetailApi/GetAllIncidents
        public ActionResult<IEnumerable<FraudulentIncidentDetail>> GetAllIncidents()
        {
            var incidents = _fraudulentIncidentBo.FindAllFraudulentIncidents();
            var formattedIncidents = new List<object>();

            foreach (var incident in incidents)
            {
                var formattedIncident = new
                {
                    IncidentNumber = incident.IncidentNumber,
                    TransactionReferenceNumber = incident.TransactionReferenceNumber,
                    IncidentStatus = incident.IncidentStatus,
                    FraudulentType = incident.FraudulentType,
                    Comments = incident.Comments
                };

                formattedIncidents.Add(formattedIncident);
            }

            return Ok(formattedIncidents);
        }


        [Route("UpdateIncident/{incidentId}")]
        [HttpPut]
        public ActionResult UpdateIncident(int incidentId, [FromBody] FraudulentIncidentDetail updatedIncident)
        {
            try
            {
                if (updatedIncident == null)
                {
                    return BadRequest(new { Status = "Error", Message = "Invalid input data" });
                }

                // Retrieve existing transaction details based on the transaction number
                var existingIncident = _fraudulentIncidentBo.FindFraudulentIncident(incidentId);

                if (existingIncident == null)
                {
                    return NotFound(new { Status = "Error", Message = "Transaction not found" });
                }

                // Update existing transaction details with the provided values
                existingIncident.IncidentStatus = updatedIncident.IncidentStatus;
                existingIncident.FraudulentType = updatedIncident.FraudulentType;

                // Perform the update
                int updatedIncidentId = _fraudulentIncidentBo.UpdateFraudulentIncident(existingIncident);

                if (updatedIncidentId > 0)
                {
                    return Ok(new { Status = "Success", Message = "Transaction details updated successfully", IncidentNumber = updatedIncidentId });
                }
                else
                {
                    return BadRequest(new { Status = "Error", Message = "Failed to update transaction details" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
            }
        }


        [Route("GetFilteredIncidents")]
        [HttpGet]
        //https://localhost:7270/api/FraudulentIncidentDetailApi/GetFilteredIncidents
        public ActionResult<IEnumerable<FraudulentIncidentDetail>> GetFilteredIncidents()
        {
            var incidents = _fraudulentIncidentBo.FilterFraudulentIncidents();
            return Ok(incidents);
        }


    }
}
