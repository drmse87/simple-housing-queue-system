using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using csharp_asp_net_core_mvc_housing_queue.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace csharp_asp_net_core_mvc_housing_queue.Controllers
{
    public class ListingsController : Controller
    {
        private readonly ILogger<ListingsController> _logger;
        private readonly Data.ApplicationDbContext _context;

        public ListingsController(ILogger<ListingsController> logger,
        Data.ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var allOpenListings = await _context.OpenListings.ToListAsync();
            IEnumerable<OpenListingViewModel> model = allOpenListings.Select(o => {
                return new OpenListingViewModel {
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

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var specificListingDetail = await _context.ListingDetails.FromSqlRaw(@"SELECT ListingID, L.RentalObjectID, Rent, RentalObjectType, 
                                                Floor, FloorPlanUrl, Rooms, Size, StreetAddress, 
                                                P.Description AS PropertyDescription, PropertyPhotoUrl, 
                                                A.Description AS AreaDescription, Name, PublishDate, 
                                                LastApplicationDate, MoveInDate 
                                                FROM Listings AS L
                                                INNER JOIN RentalObjects as RO
                                                    ON RO.RentalObjectID=L.RentalObjectID
                                                INNER JOIN Properties AS P 
                                                    ON P.PropertyID=RO.PropertyID
                                                INNER JOIN Areas AS A
                                                    ON P.AreaID=A.AreaID
                                                    WHERE L.ListingID = {0}", id)
                                                    .FirstOrDefaultAsync();

            if (specificListingDetail == null) return new NotFoundResult();

            ListingDetailViewModel model = new ListingDetailViewModel {
                    ListingID = specificListingDetail.ListingID,
                    RentalObjectID = specificListingDetail.RentalObjectID,
                    RentalObjectType = specificListingDetail.RentalObjectType,
                    Floor = specificListingDetail.Floor.GetType()?
                                    .GetMember(specificListingDetail.Floor.ToString())?
                                    .First()
                                    .GetCustomAttribute<DisplayAttribute>()?
                                    .Name,
                    Rooms = specificListingDetail.Rooms.GetType()?
                                    .GetMember(specificListingDetail.Rooms.ToString())?
                                    .First()
                                    .GetCustomAttribute<DisplayAttribute>()?
                                    .Name,
                    Rent = specificListingDetail.Rent.ToString("F"),
                    Size = specificListingDetail.Size.ToString("F"),
                    FloorPlanUrl = specificListingDetail.FloorPlanUrl,
                    StreetAddress = specificListingDetail.StreetAddress,
                    PropertyDescription = specificListingDetail.PropertyDescription,
                    PropertyPhotoUrl = specificListingDetail.PropertyPhotoUrl,
                    AreaDescription = specificListingDetail.AreaDescription,
                    Name = specificListingDetail.Name,
                    PublishDate = specificListingDetail.PublishDate.ToShortDateString(),
                    LastApplicationDate = specificListingDetail.LastApplicationDate.ToShortDateString(),
                    MoveInDate = specificListingDetail.MoveInDate.ToShortDateString()                                     
                };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MakeNewApplication(string id)
        {
            string listingId = id;
            string userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if user has previously applied for object.
            var previouslyAppliedForObject = await _context.Applications
                .FromSqlInterpolated($"SELECT * FROM Applications WHERE ListingID = {listingId} AND UserId = {userId}")
                .FirstOrDefaultAsync();
            if (previouslyAppliedForObject != null)
            {
                TempData["Message"] = "You have already applied for this object.";
                return RedirectToAction("Index");
            }

            // Check if user already has active contract.
            var activeContract = await _context.Contracts
                                            .FromSqlInterpolated($"SELECT * FROM Contracts WHERE UserID = {userId} AND EndDate IS NULL OR EndDate > GETDATE();")
                                            .FirstOrDefaultAsync();
            if (previouslyAppliedForObject != null)
            {
                TempData["Message"] = "You already have an active contract and are unable to apply for new objects.";
                return RedirectToAction("Index");
            }

            // Make a new application.
            string applicationId = Guid.NewGuid().ToString();
            DateTime applicationDate = DateTime.Now;
            await _context.Database.ExecuteSqlRawAsync(@"INSERT INTO Applications 
                                            (ApplicationID, UserID, ListingID, ApplicationDate) 
                                            VALUES ({0}, {1}, {2}, {3})", applicationId, userId, listingId, applicationDate);
            TempData["Message"] = "Successfully applied for object.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
