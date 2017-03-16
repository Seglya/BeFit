using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IAppUserRepository
    {
       IEnumerable<AppUser> UsersProfile { get; }
        Task<AppUser> SaveUserProfileAsync(string key, UserProfileViewModel viewModel,int? id);
        Task<AppUser> GetUserProfileById(int id);
    }

    public class AppUserRepository : IAppUserRepository
    {
        private BeFitDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AppUserRepository(BeFitDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager  )
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IEnumerable<AppUser> UsersProfile => _context.AppUser.AsNoTracking();

        public async Task<AppUser> GetUserProfileById(int id)
        {
            if(id>0)
            return await _context.AppUser.FindAsync(id);
            return null;
        }

        public async Task<AppUser> SaveUserProfileAsync(string key, UserProfileViewModel viewModel, int? id)
        {
            if (key == null & (id == null || id == 0))
            {
                return null;
            }
            else if (key != null & id > 0)
            {
                return null;
            }
            if(viewModel==null)
            {
                return null;
            }
            
            
            return 


        }
    }

}
