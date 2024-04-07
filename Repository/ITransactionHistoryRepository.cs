using FraudDetectionRepositoryPatternProject.Models;

namespace FraudDetectionRepositoryPatternProject.Repository
{
    public interface ITransactionHistoryRepository
    {
        int InsertTransactionHistory(TransactionHistory transactionHistory);
        TransactionHistory FindTransactionHistory(int transactionNumber);
        IEnumerable<TransactionHistory> FindAllTransactions();
        int UpdateTransactionHistory(TransactionHistory transactionHistory);
        IEnumerable<TransactionHistory> FindAllTransactionHistoriesUsingJoins();
        IEnumerable<TransactionHistory> FilterAllTransactions();
        IEnumerable<TransactionHistory> FindTransactionsUsingPaymentMethodAndAmount(decimal amount, string paymentMethod);
        
    }
}
