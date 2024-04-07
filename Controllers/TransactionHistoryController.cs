using FraudDetectionRepositoryPatternProject.BO;
using FraudDetectionRepositoryPatternProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FraudDetectionRepositoryPatternProject.Controllers
{
    public class TransactionHistoryController : Controller
    {

        private TransactionHistoryBo _transactionHistoryBo = null;

        public TransactionHistoryController(TransactionHistoryBo transactionHistoryBo)
        {
            _transactionHistoryBo = transactionHistoryBo;
        }

        // GET: TransactionHistoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionHistoryController/Details/5
        public ActionResult Details(int transactionNumber)
        {
            TransactionHistory transaction = _transactionHistoryBo.FindTransactionHistory(transactionNumber);
            if (transaction != null)
            {
                ViewData["Response"] = "Transaction History Details fetched successfully!!!";
            }
            else
            {
                ViewData["Response"] = "Failed to fetch Transaction History Details...";
            }
            return View("Views/TransactionHistory/Details.cshtml", transaction);
        }

        // GET: TransactionHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionHistory transactionHistory)
        {
            int transactionNumber = _transactionHistoryBo.InsertTransactionHistory(transactionHistory);

            if (transactionNumber > 0)
            {
                ViewData["Response"] = "Transaction History Added successfully!!!";
            }
            else
            {
                ViewData["Response"] = "Failed to add Transaction History Details...";
            }

            return View("Views/TransactionHistory/Create.cshtml", transactionHistory);
        }

        // GET: TransactionHistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the existing transaction details based on the id
            TransactionHistory existingTransaction = _transactionHistoryBo.FindTransactionHistory(id);

            // Check if the transaction with the given id exists
            if (existingTransaction == null)
            {
                ViewData["Response"] = "Transaction Reference Number Not Found...";
            }

            // Pass the existing transaction details to the view for editing
            return View("Views/TransactionHistory/Edit.cshtml", existingTransaction);
        }

        // POST: TransactionHistoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransactionHistory transaction)
        {
            Console.WriteLine("Update method trigerred {0}", transaction);
            //transaction.ModifiedAt = DateTime.Now;
            int transactionNumber = _transactionHistoryBo.UpdateTransactionHistory(transaction);
            Console.WriteLine("Transaction history updated with transaction reference number {0}", transactionNumber);

            if (transactionNumber > 0)
            {
                ViewData["Response"] = "Transaction History updated successfully!!!";
            }
            else
            {
                ViewData["Response"] = "Failed to update Transaction History Details...";
            }

            return View("Views/TransactionHistory/Edit.cshtml", transaction);
        }

        // GET: TransactionHistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionHistoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult FindAllTransactions()
        {
            IEnumerable<TransactionHistory> transactions = _transactionHistoryBo.FindAllTransactions();
            return View("FindAllTransactions", transactions);
        }

        public IActionResult TransactionsWithIncidents()
        {
            IEnumerable<TransactionHistory> transactions = _transactionHistoryBo.FindAllTransactionHistoriesUsingJoins();
            return View("TransactionsWithIncidents", transactions);
        }

        public IActionResult FilterTransactions()
        {
            IEnumerable<TransactionHistory> transactions = _transactionHistoryBo.FilterAllTransactions();
            return View("FilterTransactions", transactions);
        }

        public IActionResult TransactionsJoinIncidents(decimal amount, string paymentMethod)
        {
            var transactionIncidents = _transactionHistoryBo.FindTransactionsUsingPaymentMethodAndAmount(amount, paymentMethod);
            return View("TransactionsJoinIncidents", transactionIncidents);
        }


    }
}
