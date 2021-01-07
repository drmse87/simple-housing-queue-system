using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
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

        public IActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public IActionResult NewArea()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewArea(AreaEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid input.";
                return View();
            }

            var id = Guid.NewGuid().ToString();
            var areaName = model.Name;
            var areaDescription = model.Description;

            TempData["Message"] = "Added new area successfully.";
            await _context.Database
                .ExecuteSqlRawAsync("INSERT INTO Areas (AreaID, Name, Description) VALUES ({0}, {1}, {2})", id, areaName, areaDescription);
               
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
