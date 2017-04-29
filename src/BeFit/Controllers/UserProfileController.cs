using System.IO;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    [Authorize]
    public class UserProfileController : Controller

    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileController(IAppUserRepository appUserRepository, UserManager<ApplicationUser> userManager)
        {
            _appUserRepository = appUserRepository;
            _userManager = userManager;
        }

        // GET: /NewUser/
        public IActionResult NewUser()
        {
            ViewData["Title"] = "New Profile";
            return View(new UserProfileViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit prifile";
            var user = await _appUserRepository.GetUserProfileById(id);
            var viewModel = new UserProfileViewModel
            {
                FirstName = user.FirstName,
                Sex = user.Sex,
                ImagePath = user.ImagePath,
                CurrentWeight = user.CurrentWeight,
                DateOfBirth = user.DateOfBirth,
                DateOfRegisrtration = user.DateOfRegoistration,
                Goal = user.Goal,
                SecondName = user.SecondName,
                WeeksForGoal = user.WeeksForGoal
            };
            return View("NewUser", viewModel);
        }

        //Post: NewUser/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUser(UserProfileViewModel viewModel, int? id)
        {
            if (id == null)
                id = 0;
            if (viewModel.DateOfBirth.Year < 1920)
                ModelState.AddModelError("DateOfBirth", "Input a valid date of birth!!!");

            if (ModelState.IsValid)
                try
                {
                    if (viewModel.IFile != null)
                    {
                        var Path = @"../BeFit/wwwroot/photo/" + viewModel.IFile.FileName;

                        using (var stream = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite))
                        {
                            await viewModel.IFile.CopyToAsync(stream);
                            viewModel.ImagePath = @"/photo/" + viewModel.IFile.FileName;
                            ;
                        }


                        viewModel.ImageName = viewModel.IFile.FileName;
                    }
                    var key = GetCurrentUserAsync().Result.Id;
                    var newUserPrifile = await _appUserRepository.SaveUserProfileAsync(key, viewModel, (int) id);
                    return RedirectToAction("Profile", new {id = newUserPrifile.AppUserID});
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists " +
                                                 "see your system administrator.");
                }
            return View();
        }

        public async Task<IActionResult> Profile(int id = 0)

        {
            if (id == 0)
            {
                var key = GetCurrentUserAsync().Result.Id;
                if (key == null)
                    return NotFound();
                var user = await _appUserRepository.GetUserByKeyAsync(key);
                return View(user);
            }
            else
            {
                var user = await _appUserRepository.GetUserProfileById(id);
                if (user == null)
                    return NotFound();
                return View(user);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}