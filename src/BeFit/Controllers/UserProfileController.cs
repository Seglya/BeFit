using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models.UserProfileViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class UserProfileController : Controller

    {
        private IAppUserRepository _appUserRepository;

        public UserProfileController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        // GET: /NewUser/
        public IActionResult NewUser()
        {
            return View();
        }
        //Post: NewUser/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewUser(UserProfileViewModel viewModel)
        {
            if (viewModel.DateOfBirth.Year<1920)
            {
                ModelState.AddModelError("DateOfBirth","Input a valid date of birth!!!");
            }
            if (ModelState.IsValid)
            {
                viewModel.DateOfRegoistration = System.DateTime.Now;
                try
                {
                    var newUserPrifile = await  _appUserRepository.SaveUserProfileAcync("1",viewModel,0);
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
    }
}
