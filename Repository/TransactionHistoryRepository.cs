using FraudDetectionRepositoryPatternProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FraudDetectionRepositoryPatternProject.Repository
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly FraudRiskManagementContext _context;

        //Initializing the FraudRiskManagementContext instance which is received as a arugument
        public TransactionHistoryRepository(FraudRiskManagementContext context)
        {
            _context = context;
        }

        //intializing the FraudRiskManagementContext instance
        public TransactionHistoryRepository()
        {
            _context = new FraudRiskManagementContext();
        }

        public int InsertTransactionHistory(TransactionHistory transactionHistory)
        {
            _context.TransactionHistories.Add(transactionHistory);
            _context.SaveChanges();
            return transactionHistory.TransactionReferenceNumber;
        }

        public TransactionHistory FindTransactionHistory(int transactionNumber)
        {
            return _context.TransactionHistories.Find(transactionNumber);
        }

        public IEnumerable<TransactionHistory> FindAllTransactions()
        {
            return _context.TransactionHistories.ToList();
        }

        public int UpdateTransactionHistory(TransactionHistory transactionHistory)
        {
            _context.Entry(transactionHistory).State = EntityState.Modified;
            _context.SaveChanges();
            return transactionHistory.TransactionReferenceNumber;
        }

        public IEnumerable<TransactionHistory> FindAllTransactionHistoriesUsingJoins()
        {
            var history = _context.TransactionHistories.Include("FraudulentIncidentDetails").ToList();
            return history;
        }

        public IEnumerable<TransactionHistory> FilterAllTransactions()
        {
            IEnumerable<TransactionHistory> transactions = (from transaction in _context.TransactionHistories
                                                            where transaction.Amount > 70000 && transaction.SenderName.Length>3 && transaction.BeneficiaryName.Contains("i")
                                                            select transaction);
            return transactions;
        }

        public IEnumerable<TransactionHistory> FindTransactionsUsingPaymentMethodAndAmount(decimal amount, string paymentMethod)
        {
            var transactionIncident = (from transaction in _context.TransactionHistories
                                       join incident in _context.FraudulentIncidentDetails
                                       on transaction.TransactionReferenceNumber equals incident.TransactionReferenceNumber
                                       where transaction.Amount >= amount && transaction.PaymentMethod == paymentMethod
                                       select new TransactionHistory
                                       {
                                           TransactionReferenceNumber = transaction.TransactionReferenceNumber,
                                           Amount = transaction.Amount,
                                           SenderName = transaction.SenderName,
                                           BeneficiaryName = transaction.BeneficiaryName,
                                           FraudulentIncidentDetails = new List<FraudulentIncidentDetail>
                                           {
                                               new FraudulentIncidentDetail
                                               {
                                                   IncidentNumber = incident.IncidentNumber,
                                                   FraudulentType = incident.FraudulentType
                                               }
                                           }
                                       });

            return transactionIncident;
        }

        

    }
}
