using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using csharp_asp_net_core_mvc_housing_queue.Models;

namespace csharp_asp_net_core_mvc_housing_queue.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly Data.ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger,
                            Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rentalObjectsPerAreaCounts = await _context.RentalObjectsPerAreaCounts
                                                         .FromSqlInterpolated(
                                                        $@"SELECT A.Name AS AreaName, COUNT(RentalObjectID) AS NumberOfRentalObjectsInArea
                                                        FROM RentalObjects AS RO
                                                        INNER JOIN Properties AS P 
                                                            ON RO.PropertyID=P.PropertyID
                                                        INNER JOIN Areas AS A
                                                            ON P.AreaID=A.AreaID
                                                        GROUP BY A.Name")
                                                        .ToListAsync();

            var rentalObjectsWithoutContracts = await _context.RentalObjects
                                                         .FromSqlInterpolated(
                                                        $@"SELECT * FROM RentalObjects
                                                        WHERE RentalObjectID NOT IN 
                                                            (SELECT RentalObjectID 
                                                            FROM Contracts 
                                                            WHERE EndDate IS NULL OR EndDate > GETDATE())")
                                                        .ToListAsync();

            var rentalObjectsWithoutContractsAndListings = await _context.RentalObjects
                                                         .FromSqlInterpolated(
                                                        $@"SELECT * FROM RentalObjects
                                                            WHERE RentalObjectID NOT IN 
                                                                (SELECT RentalObjectID 
                                                                FROM Contracts 
                                                                WHERE EndDate IS NULL OR EndDate > GETDATE())
                                                            AND RentalObjectID NOT IN 
                                                                (SELECT RentalObjectID 
                                                                FROM Listings 
                                                                WHERE LastApplicationDate > GETDATE() AND ListingClosureDate IS NULL)
                                                            ")
                                                        .ToListAsync();
        
            var allActiveContracts = await _context.ActiveContracts
                                                        .FromSqlRaw(
                                                        @"SELECT U.FirstName, U.LastName, 
                                                        RO.RentalObjectID, C.StartDate, C.EndDate 
                                                        FROM Contracts as C
                                                        INNER JOIN AspNetUsers AS U
                                                            ON C.UserId=U.Id
                                                        INNER JOIN RentalObjects as RO
                                                            ON C.RentalObjectID=RO.RentalObjectID
                                                        WHERE EndDate IS NULL OR EndDate > GETDATE()")
                                                        .ToListAsync();

            var allOpenListings = await _context.OpenListings.ToListAsync();

            IEnumerable<AdminListingViewModel> allListingsAndQueingApplicants = allOpenListings
            .Select(o => {
                var queuingApplicants = _context.QueuingApplicantsDetails
                                                            .FromSqlInterpolated(
                                                                $@"SELECT UserId, FirstName, LastName, 
                                                                RegistrationDate, ApplicationDate, 
                                                                DATEDIFF(DAY, RegistrationDate, GETDATE()) AS QueueTime
                                                                FROM Applications AS A
                                                                INNER JOIN AspNetUsers AS U
                                                                    ON A.UserId=U.Id
                                                                WHERE ListingID = {o.ListingID}
                                                                ORDER BY QueueTime DESC, ApplicationDate ASC OFFSET 0 ROWS")
                                                            .Select(q => new QueuingApplicantViewModel {
                                                                            UserId = q.UserId,
                                                                            FirstName = q.FirstName,
                                                                            LastName = q.LastName,
                                                                            RegistrationDate = q.RegistrationDate.ToShortDateString(),
                                                                            ApplicationDate = q.ApplicationDate.ToShortDateString(),
                                                                            QueueTime = q.QueueTime.ToString()
                                                                        });

                    return new AdminListingViewModel {
                        QueuingApplicants = queuingApplicants,
                        RentalObjectID = o.RentalObjectID,
                        ListingID = o.ListingID,
                        Name = o.Name,
                        Rooms = o.Rooms.GetType()?
                                        .GetMember(o.Rooms.ToString())?
                                        .First()
                                        .GetCustomAttribute<DisplayAttribute>()?
                                        .Name,
                        Size = o.Size.ToString("F"),
                        Rent = o.Rent.ToString("F"),
                        StreetAddress = o.StreetAddress,
                        PropertyPhotoUrl = o.PropertyPhotoUrl,
                        PublishDate = o.PublishDate.ToShortDateString(),
                        LastApplicationDate = o.LastApplicationDate.ToShortDateString(),
                        MoveInDate = o.MoveInDate.ToShortDateString()
                        };
                });

            AdminViewModel model = new AdminViewModel {
                ActiveContracts = allActiveContracts,
                AllListingsAndQueingApplicants = allListingsAndQueingApplicants,
                NewContractEditViewModel = new NewContractEditViewModel(),
                RentalObjectsPerAreaCounts = rentalObjectsPerAreaCounts,
                RentalObjectsWithoutContracts = rentalObjectsWithoutContracts,
                RentalObjectsWithoutContractsAndListings = rentalObjectsWithoutContractsAndListings,
            };            

            return View(model);
        }

        [HttpGet]
        public IActionResult NewListing()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewListing(NewListingEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid input.";
                return View();
            }

            string listingId = Guid.NewGuid().ToString();
            DateTime publishDate = DateTime.Now;
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO Listings 
                                                                (ListingID, RentalObjectID, PublishDate, 
                                                                LastApplicationDate, MoveInDate) 
                                                                VALUES ({listingId}, {model.RentalObjectID}, 
                                                                {publishDate}, {model.LastApplicationDate}, 
                                                                {model.MoveInDate})");
            TempData["Message"] = "Successfully created new listing.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult NewContract()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewContract(NewContractEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid input.";
                return View();
            }

            // First check if user already has active contract.
            var activeContract = await _context.Contracts
                                            .FromSqlInterpolated($"SELECT * FROM Contracts WHERE UserID = {model.UserID} AND EndDate IS NULL OR EndDate > GETDATE()")
                                            .FirstOrDefaultAsync();
            if (activeContract != null)
            {
                TempData["Message"] = "User already has an active contract.";
                return RedirectToAction("Index");
            }

            // Then update listing closure date.
            DateTime now = DateTime.Now;
            await _context.Database.ExecuteSqlInterpolatedAsync($@"UPDATE Listings
                                                                SET ListingClosureDate = {now}
                                                                WHERE ListingID = {model.ListingID}");

            // Finally award contract.
            string contractId = Guid.NewGuid().ToString();
            DateTime awardedDate = DateTime.Now;
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO Contracts 
                                                                (ContractID, UserId, RentalObjectID, 
                                                                StartDate, ContractAwardedDate) 
                                                                VALUES ({contractId}, {model.UserID}, 
                                                                {model.RentalObjectID}, {model.StartDate}, {awardedDate})");
            TempData["Message"] = "Successfully awarded contract.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
