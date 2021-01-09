using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
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
            var rentalObjectsWithoutContracts = await _context.RentalObjects
                                                         .FromSqlInterpolated(
                                                        $@"SELECT * FROM RentalObjects
                                                        WHERE RentalObjectID NOT IN 
                                                            (SELECT RentalObjectID 
                                                            FROM Contracts 
                                                            WHERE EndDate IS NULL OR EndDate > GETDATE());")
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
                                                                WHERE LastApplicationDate > GETDATE())
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
                                                        WHERE EndDate IS NULL OR EndDate > GETDATE();")
                                                        .ToListAsync();

            var allOpenListings = await _context.OpenListings.ToListAsync();

            IEnumerable<AdminListingViewModel> allListingsAndQueingApplicants = allOpenListings.Select(o => {
                var queuingApplicants = _context.QueuingApplicants
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
                RentalObjectsWithoutContracts = rentalObjectsWithoutContracts,
                RentalObjectsWithoutContractsAndListings = rentalObjectsWithoutContractsAndListings,
                ActiveContracts = allActiveContracts,
                AllListingsAndQueingApplicants = allListingsAndQueingApplicants,
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
                                                                VALUES ({listingId}, {model.RentalObjectId}, 
                                                                {publishDate}, {model.LastApplicationDate}, 
                                                                {model.MoveInDate})");
            TempData["Message"] = "Successfully created new listing.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
