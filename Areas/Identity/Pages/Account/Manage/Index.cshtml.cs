using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using simple_housing_queue_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace simple_housing_queue_system.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }
        public IEnumerable<MadeApplicationViewModel> MadeApplications { get; set; }
        
        [Display(Name = "Registration Date")]
        public string RegistrationDate { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Display(Name = "Street Address")]
            public string StreetAddress { get; set; }
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var FirstName = user.FirstName;
            var LastName = user.LastName;
            var StreetAddress = user.StreetAddress;

            RegistrationDate = user.RegistrationDate.ToShortDateString();

            string userId = _userManager.GetUserId(User);
            var allMadeApplications = _context.MadeApplications
                                                .FromSqlInterpolated(
                                                    $@"SELECT L.ListingID, RO.RentalObjectID, 
                                                    RO.Rooms, RO.Size, RO.Rent, P.StreetAddress, 
                                                    A.ApplicationDate, L.LastApplicationDate, L.MoveInDate
                                                        FROM Applications AS A
                                                        INNER JOIN Listings as L
                                                            ON A.ListingID = L.ListingID
                                                        INNER JOIN RentalObjects as RO
                                                            ON L.RentalObjectID = RO.RentalObjectID
                                                        INNER JOIN Properties as P
                                                            ON RO.PropertyID = P.PropertyID
                                                        WHERE UserId = {userId}")
                                                    .ToList();
            
            MadeApplications = allMadeApplications.Select(a => {
                var applicantQueuePosition = _context.QueingApplicantPositions
                                                        .FromSqlInterpolated(
                                                            $@"SELECT PlaceInQueue FROM
                                                                (SELECT UserId, ROW_NUMBER() OVER (ORDER BY QueueTime DESC, ApplicationDate ASC) AS PlaceInQueue
                                                                FROM
                                                                    (SELECT UserId, ApplicationDate, DATEDIFF(DAY, RegistrationDate, GETDATE()) AS QueueTime
                                                                    FROM Applications AS A
                                                                    INNER JOIN AspNetUsers AS U
                                                                        ON A.UserId=U.Id
                                                                    WHERE ListingID = {a.ListingID}
                                                                        ) AS B
                                                                ) as A
                                                            WHERE UserId = {userId}")
                                                        .ToList();

                var applicantsInQueueCount = _context.QueingApplicantCounts
                                                        .FromSqlInterpolated(
                                                            $@"SELECT COUNT(UserId) AS ApplicantsCount
                                                                FROM Applications
                                                                WHERE ListingID = {a.ListingID}
                                                                GROUP BY UserId")
                                                        .ToList();

                return new MadeApplicationViewModel {
                    ListingID = a.ListingID,
                    RentalObjectID = a.RentalObjectID,
                    Rooms = a.Rooms.GetType()?
                                    .GetMember(a.Rooms.ToString())?
                                    .First()
                                    .GetCustomAttribute<DisplayAttribute>()?
                                    .Name,
                    Size = a.Size.ToString("F"),
                    Rent = a.Rent.ToString("F"),
                    StreetAddress = a.StreetAddress,
                    ApplicationDate = a.ApplicationDate.ToShortDateString(),
                    LastApplicationDate = a.LastApplicationDate.ToShortDateString(),
                    MoveInDate = a.LastApplicationDate.ToShortDateString(),
                    PlaceInQueue = applicantQueuePosition.First().PlaceInQueue.ToString() ?? "0",
                    TotalApplicantsInQueue = applicantsInQueueCount.First().ApplicantsCount.ToString() ?? "0"
                };
            });

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = FirstName,
                LastName = LastName,
                StreetAddress = StreetAddress
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.StreetAddress = Input.StreetAddress;
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
