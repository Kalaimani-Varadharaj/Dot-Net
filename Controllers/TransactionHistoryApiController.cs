using FraudDetectionRepositoryPatternProject.BO;
using FraudDetectionRepositoryPatternProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FraudDetectionRepositoryPatternProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryApiController : ControllerBase
    {
        private TransactionHistoryBo _transactionHistoryBo;

        public TransactionHistoryApiController(TransactionHistoryBo transactionHistoryBo)
        {
            _transactionHistoryBo = transactionHistoryBo;
        }

        [Route("AddTransaction")]
        [HttpPost]
        public ActionResult<IEnumerable<TransactionHistory>> AddTransaction(TransactionHistory transactionHistory)
        {
            //TransactionHistoryAnnotations transaction = new TransactionHistoryAnnotations
            //{
            //    TransactionReferenceNumber = transactionHistory.TransactionReferenceNumber,
            //    TransactionDateTime = transactionHistory.TransactionDateTime,
            //    Amount = transactionHistory.Amount,
            //    SenderAccountNumber = transactionHistory.SenderAccountNumber,
            //    BeneficiaryAccountNumber = transactionHistory.BeneficiaryAccountNumber,
            //    SenderName = transactionHistory.SenderName,
            //    BeneficiaryName = transactionHistory.BeneficiaryName,
            //    PaymentMethod = transactionHistory.PaymentMethod,
            //    // Assign other properties accordingly
            //};

            var validationResults = new List<ValidationResult>();
            //bool isValid = Validator.TryValidateObject(transaction, new ValidationContext(transaction), validationResults, true);

            //if (isValid)
            //{
                
            //}
            //else
            //{
            //    // If validation fails, return BadRequest with validation errors
            //    var validationErrors = validationResults.Select(result => result.ErrorMessage);
            //    return BadRequest(new { Status = "Error", Message = "Validation failed", Errors = validationErrors });
            //}

            try
            {
                int transactionNumber = _transactionHistoryBo.InsertTransactionHistory(transactionHistory);
                if (transactionNumber > 0)
                {
                    return Ok(new { Status = "Success", Message = "Transaction Details added successfully", TransactionNumber = transactionNumber });
                }
                else
                {
                    return BadRequest(new { Status = "Error", Message = "Failed to add transaction details" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
            }
        }

        [Route("GetTransaction/{transactionNumber}")]
        [HttpGet]
        // https://localhost:7270/api/TransactionHistoryApi/GetTransaction/123
        public ActionResult GetTransaction(int transactionNumber)
        {
            try
            {
                var transaction = _transactionHistoryBo.FindTransactionHistory(transactionNumber);

                if (transaction != null)
                {
                    return Ok(transaction);
                }
                else
                {
                    return NotFound(new { Status = "Error", Message = "Transaction not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
            }
        }


        [Route("GetAllTransactions")]
        [HttpGet]
        //https://localhost:7270/api/TransactionHistoryApi/GetAllTransactions
        public ActionResult<IEnumerable<TransactionHistory>> GetAllTransactions()
        {
            var transactions = _transactionHistoryBo.FindAllTransactions();
            var formattedTransactions = new List<object>();

            foreach (var transaction in transactions)
            {
                var formattedTransaction = new
                {
                    TransactionReferenceNumber = transaction.TransactionReferenceNumber,
                    TransactionDateTime = transaction.TransactionDateTime,
                    amount = transaction.Amount,
                    SenderAccountNumber = transaction.SenderAccountNumber,
                    BeneficiaryAccountNumber = transaction.BeneficiaryAccountNumber,
                    SenderName = transaction.SenderName,
                    BeneficiaryName = transaction.BeneficiaryName,
                    PaymentMethod = transaction.PaymentMethod,
                    CreatedAt = transaction.CreatedAt,
                    ModifiedAt = transaction.ModifiedAt,
                    // Add other properties as needed
                };

                formattedTransactions.Add(formattedTransaction);
            }

            return Ok(formattedTransactions);
        }

        [Route("UpdateTransaction/{transactionNumber}")]
        [HttpPut]
        // https://localhost:7270/api/TransactionHistoryApi/UpdateTransaction/123
        public ActionResult UpdateTransaction(int transactionNumber, TransactionHistory updatedTransaction)
        {
            TransactionUpdateValidationModel transaction = new TransactionUpdateValidationModel
            {
                Amount = updatedTransaction.Amount,
                SenderName = updatedTransaction.SenderName,
                BeneficiaryName = updatedTransaction.BeneficiaryName,
                PaymentMethod = updatedTransaction.PaymentMethod,
               
            };

            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(transaction, new ValidationContext(transaction), validationResults, true);

            if (isValid)
            {
                try
                {
                    // Retrieve existing transaction details based on the transaction number
                    var existingTransaction = _transactionHistoryBo.FindTransactionHistory(transactionNumber);

                    if (existingTransaction == null)
                    {
                        return NotFound(new { Status = "Error", Message = "Transaction not found" });
                    }

                    // Update existing transaction details with the provided values
                    existingTransaction.Amount = updatedTransaction.Amount;
                    existingTransaction.SenderName = updatedTransaction.SenderName;
                    existingTransaction.BeneficiaryName = updatedTransaction.BeneficiaryName;
                    existingTransaction.PaymentMethod = updatedTransaction.PaymentMethod;
                    existingTransaction.ModifiedAt = updatedTransaction.ModifiedAt;

                    // Perform the update
                    int updatedTransactionNumber = _transactionHistoryBo.UpdateTransactionHistory(existingTransaction);

                    if (updatedTransactionNumber > 0)
                    {
                        return Ok(new { Status = "Success", Message = "Transaction details updated successfully", TransactionNumber = updatedTransactionNumber });
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
            else
            {
                // If validation fails, return BadRequest with validation errors
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(new { Status = "Error", Message = "Validation failed", Errors = validationErrors });
            }
        }


        [Route("GetTransactionsWithIncidents")]
        [HttpGet]
        //https://localhost:7270/api/TransactionHistoryApi/GetTransactionsWithIncidents
        public ActionResult<IEnumerable<TransactionHistory>> GetTransactionsWithIncidents()
        {
            var transactions = _transactionHistoryBo.FindAllTransactionHistoriesUsingJoins();
            var formattedTransactions = new List<object>();

            foreach (var transaction in transactions)
            {
                var formattedTransaction = new
                {
                    TransactionReferenceNumber = transaction.TransactionReferenceNumber,
                    TransactionDateTime = transaction.TransactionDateTime,
                    amount = transaction.Amount,
                    SenderAccountNumber = transaction.SenderAccountNumber,
                    BeneficiaryAccountNumber = transaction.BeneficiaryAccountNumber,
                    SenderName = transaction.SenderName,
                    BeneficiaryName = transaction.BeneficiaryName,
                    PaymentMethod = transaction.PaymentMethod,
                    CreatedAt = transaction.CreatedAt,
                    ModifiedAt = transaction.ModifiedAt,
                    Incidents = transaction.FraudulentIncidentDetails.Select(incident => new
                    {
                        IncidentNumber = incident.IncidentNumber,
                        FraudulentType = incident.FraudulentType,
                        IncidentStatus = incident.IncidentStatus,
                        Comments = incident.Comments,
                    })
                };

                formattedTransactions.Add(formattedTransaction);
            }

            return Ok(formattedTransactions);
            //return Ok(transactions);
        }

        [Route("GetFilteredTransactions")]
        [HttpGet]
        //https://localhost:7270/api/TransactionHistoryApi/GetFilteredTransactions
        public ActionResult<IEnumerable<TransactionHistory>> GetFilteredTransactions()
        {
            var transactions = _transactionHistoryBo.FilterAllTransactions();
            return Ok(transactions);
        }


        [HttpGet]
        [Route("FindTransactions")]
        // https://localhost:7270/api/TransactionHistoryApi/FindTransactions
        public IActionResult FindTransactions(decimal amount, string paymentMethod)
        {
            try
            {
                var transactions = _transactionHistoryBo.FindTransactionsUsingPaymentMethodAndAmount(amount, paymentMethod);

                if (transactions.Any())
                {
                    var formattedTransactions = transactions.Select(transaction => new
                    {
                        TransactionReferenceNumber = transaction.TransactionReferenceNumber,
                        Amount = transaction.Amount,
                        SenderName = transaction.SenderName,
                        BeneficiaryName = transaction.BeneficiaryName,
                        Incidents = transaction.FraudulentIncidentDetails.Select(incident => new
                        {
                            IncidentNumber = incident.IncidentNumber,
                            FraudulentType = incident.FraudulentType,
                        })
                    });

                    return Ok(formattedTransactions);
                }
                else
                {
                    return NotFound(new { Status = "Error", Message = "No transactions found for the given criteria" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
            }
        }

    }
}
