using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class UserProfileController : Controller

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppUserRepository _appUserRepository;

        public UserProfileController(IAppUserRepository appUserRepository, UserManager<ApplicationUser> userManager )
        {
            _appUserRepository = appUserRepository;
            _userManager = userManager;
        }
        // GET: /NewUser/
        public IActionResult NewUser()
        {
            ViewData["Title"] = "New Profile";
            return View();
        }

        public async Task<IActionResult> Edit()
        {
            ViewData["Title"] = "Edit prifile";
            var user = await _appUserRepository.GetUserByKeyAsync("1");//GetCurrentUserAsync().Result.Id);
            UserProfileViewModel viewModel = new UserProfileViewModel
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
        public async Task< IActionResult> NewUser(UserProfileViewModel viewModel)
        {
            if (viewModel.DateOfBirth.Year<1920)
            {
                ModelState.AddModelError("DateOfBirth","Input a valid date of birth!!!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                     if (viewModel.IFile != null)
                    {
                        string Path = @"../BeFit/wwwroot/photo/" + viewModel.IFile.FileName;
                   
                        using (var stream = new FileStream(Path, FileMode.Create,FileAccess.ReadWrite))
                        {
                            await viewModel.IFile.CopyToAsync(stream);
                            viewModel.ImagePath = @"/photo/" + viewModel.IFile.FileName; ;
                        }
                   
                   
                        viewModel.ImageName = viewModel.IFile.FileName;
                       
                    }
                    var newUserPrifile =  await _appUserRepository.SaveUserProfileAsync("1",viewModel);
                    return RedirectToAction("Profile", new {id = newUserPrifile.AppUserID});
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            return View();
        }

        public async Task<IActionResult> Profile(int id)

        {
            var user = await _appUserRepository.GetUserProfileById(id);

            return View(new UserProfileViewModel {FirstName = user.FirstName, Sex = user.Sex,ImagePath = user.ImagePath, CurrentWeight = user.CurrentWeight, DateOfBirth = user.DateOfBirth, DateOfRegisrtration = user.DateOfRegoistration, Goal = user.Goal, SecondName = user.SecondName, WeeksForGoal = user.WeeksForGoal});
        }
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
