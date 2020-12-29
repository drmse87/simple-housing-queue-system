using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using csharp_asp_net_core_mvc_housing_queue.Models;

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
            // await _context.AddAsync(new RentalObject());
            // await _context.SaveChangesAsync();

            return View();
        }

        public IActionResult Details(string id)
        {
            return View("Details");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
