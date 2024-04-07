using FraudDetectionRepositoryPatternProject.Models;
using FraudDetectionRepositoryPatternProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace FraudDetectionRepositoryPatternProject.BO
{
    public class TransactionHistoryBo
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;

        public TransactionHistoryBo(ITransactionHistoryRepository transactionHistoryRepository)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public int InsertTransactionHistory(TransactionHistory transactionHistory)
        {
            int result;
            try
            {
                result = _transactionHistoryRepository.InsertTransactionHistory(transactionHistory);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return result;
        }

        public TransactionHistory FindTransactionHistory(int transactionNumber)
        {
            TransactionHistory transaction = _transactionHistoryRepository.FindTransactionHistory(transactionNumber);
            return transaction;
        }

        public IEnumerable<TransactionHistory> FindAllTransactions()
        {
            IEnumerable<TransactionHistory> transactions = _transactionHistoryRepository.FindAllTransactions();
            return transactions;
        }

        public int UpdateTransactionHistory(TransactionHistory transactionHistory)
        {
            int result;
            try
            {
                result = _transactionHistoryRepository.UpdateTransactionHistory(transactionHistory);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return result;
        }

        public IEnumerable<TransactionHistory> FindAllTransactionHistoriesUsingJoins()
        {
            IEnumerable<TransactionHistory> list = _transactionHistoryRepository.FindAllTransactionHistoriesUsingJoins();
            return list;
        }

        public IEnumerable<TransactionHistory> FilterAllTransactions()
        {
            IEnumerable<TransactionHistory> filterTransaction = _transactionHistoryRepository.FilterAllTransactions();
            return filterTransaction;
        }

        public IEnumerable<TransactionHistory> FindTransactionsUsingPaymentMethodAndAmount(decimal amount, string paymentMethod)
        {
            IEnumerable<TransactionHistory> filtertransactions = _transactionHistoryRepository.FindTransactionsUsingPaymentMethodAndAmount(amount, paymentMethod);
            return filtertransactions;
        }

    }
}
