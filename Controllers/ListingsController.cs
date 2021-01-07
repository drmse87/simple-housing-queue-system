using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
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

        public IActionResult Index()
        {
            var allOpenListings = _context.OpenListings.ToList();
            IEnumerable<OpenListingViewModel> model = allOpenListings.Select(o => {
                return new OpenListingViewModel {
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

        public IActionResult Details(string id)
        {
            var model = _context.Listings.FromSqlRaw(@"SELECT ListingID, L.RentalObjectID, Rent, RentalObjectType, 
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
                                                .ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
