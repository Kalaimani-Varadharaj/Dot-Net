using FraudDetectionRepositoryPatternProject.BO;
using FraudDetectionRepositoryPatternProject.Models;
using FraudDetectionRepositoryPatternProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FraudDetectionRepositoryPatternProject.Controllers
{
    public class FraudulentIncidentDetailController : Controller
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly FraudulentIncidentDetailBo _fraudulentIncidentDetailBo;

        public FraudulentIncidentDetailController(ITransactionHistoryRepository transactionHistoryRepository, FraudulentIncidentDetailBo fraudulentIncidentDetailBo)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
            _fraudulentIncidentDetailBo = fraudulentIncidentDetailBo;
        }

        
        // GET: FraudulentIncidentDetailController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FraudulentIncidentDetailController/Details/5
        public ActionResult Details(int incidentId)
        {
            FraudulentIncidentDetail incident = _fraudulentIncidentDetailBo.FindFraudulentIncident(incidentId);
            if(incident != null)
            {
                ViewData["Response"] = "Fraudulent Incident Details fetched successfully!!!";
            }
            else
            {
                ViewData["Response"] = "Failed to fetch Fraudulent Incident Details, Please check the ID...";
            }

            return View("Views/FraudulentIncidentDetail/Details.cshtml", incident);
        }

       
        // GET: FraudulentIncidentDetailController/Create
        public ActionResult Create()
        {
            var transactionHistoryList = _transactionHistoryRepository.FindAllTransactions(); // Replace with your actual data retrieval logic

            var transactionReferenceNumbersList = transactionHistoryList
                .Select(transaction => new
                {
                    TransactionReferenceNumber = transaction.TransactionReferenceNumber,
                    DisplayText = $"{transaction.TransactionReferenceNumber}" // Adjust based on your actual properties
                })
                .ToList();

            ViewBag.TransactionReferenceNumbers = new SelectList(transactionReferenceNumbersList, "TransactionReferenceNumber", "DisplayText");

            return View();
        }



        // POST: FraudulentIncidentDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FraudulentIncidentDetail fraudulentIncident)
        {
            if (_fraudulentIncidentDetailBo == null)
            {
                // Log or handle the case where _fraudulentIncidentDetailBo is null
                return View("Views/FraudulentIncidentDetail/Create.cshtml", fraudulentIncident);
            }

            int incidentNumber = _fraudulentIncidentDetailBo.InsertFraudulentIncident(fraudulentIncident);

            if (incidentNumber > 0)
            {
                ViewData["Response"] = "Fraudulent Incident Added successfully!!!";
            }
            else
            {
                ViewData["Response"] = "Failed to add Fraudulent Incident Details...";
            }

            return View("Views/FraudulentIncidentDetail/Create.cshtml", fraudulentIncident);
        }


        // GET: FraudulentIncidentDetailController/Edit/5
        public ActionResult Edit(int incidentId)
        {
            // Retrieve the existing incident details based on the id
            FraudulentIncidentDetail existingIncident = _fraudulentIncidentDetailBo.FindFraudulentIncident(incidentId);

            if (existingIncident == null)
            {
                ViewData["Response"] = "Incident Number Not Found...";
                return View("Views/FraudulentIncidentDetail/Edit.cshtml", existingIncident);
            }

            // Assuming TransactionReferenceNumber is the foreign key property in FraudulentIncidentDetail
            var selectedTransactionReferenceNumber = existingIncident.TransactionReferenceNumber;

            var transactionHistoryList = _transactionHistoryRepository.FindAllTransactions();

            var transactionReferenceNumbersList = transactionHistoryList
                .Select(transaction => new
                {
                    TransactionReferenceNumber = transaction.TransactionReferenceNumber,
                    DisplayText = $"{transaction.TransactionReferenceNumber}" // Adjust based on your actual properties
                })
                .ToList();

            ViewBag.TransactionReferenceNumbers = new SelectList(transactionReferenceNumbersList, "TransactionReferenceNumber", "DisplayText", selectedTransactionReferenceNumber);

            return View("Views/FraudulentIncidentDetail/Edit.cshtml", existingIncident);
        }


        // POST: FraudulentIncidentDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FraudulentIncidentDetail fraudulentIncident)
        {
            Console.WriteLine("Update method trigerred {0}", fraudulentIncident);
            
            int incidentNumber = _fraudulentIncidentDetailBo.UpdateFraudulentIncident(fraudulentIncident);
            Console.WriteLine("Fraudulent Incident Detail updated with incident number {0}", incidentNumber);

            if (incidentNumber > 0)
            {
                ViewData["Response"] = "Fraudulent Incident Detail updated successfully!!!";
            }
            else
            {
                ViewData["Response"] = "Failed to update Fraudulent Incident Details...";
            }

            return View("Views/FraudulentIncidentDetail/Edit.cshtml", fraudulentIncident);
        }


        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FraudulentIncidentDetailController/Delete/5
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

        public IActionResult FindAllIncidents()
        {
            IEnumerable<FraudulentIncidentDetail> incidents = _fraudulentIncidentDetailBo.FindAllFraudulentIncidents();
            return View("FindAllIncidents", incidents);
        }

    }
}
