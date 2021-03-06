﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IAppUserRepository
    {
        IEnumerable<AppUser> UsersProfile { get; }
        IEnumerable<AppUser> UserProfileByFilter(string filter);
        Task<AppUser> SaveUserProfileAsync(string key, UserProfileViewModel viewModel, int id);
        Task<AppUser> GetUserProfileById(int id);
        Task<AppUser> GetUserByKeyAsync(string key);
    }

    public class AppUserRepository : IAppUserRepository
    {
        private readonly BeFitDbContext _context;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AppUserRepository(BeFitDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IEnumerable<AppUser> UsersProfile => _context.AppUser.AsNoTracking();
        public IEnumerable<AppUser> UserProfileByFilter(string filter)
        {
            if (filter != null)
                return _context.AppUser.Where(s => s.FirstName.Contains(filter)||s.SecondName.Contains(filter)).AsNoTracking();
            return _context.AppUser.AsNoTracking();
        }

        public async Task<AppUser> GetUserProfileById(int id)
        {
            if (id > 0)
                return await _context.AppUser.Include(f => f.Measurements). ThenInclude(m=>m.Measurement).SingleOrDefaultAsync(d => d.AppUserID == id);
            return null;
        }

        public async Task<AppUser> SaveUserProfileAsync(string key, UserProfileViewModel viewModel, int id)
        {
            if ((key == null) & (id == 0))
                return null;
            if (viewModel == null)
                return null;
            AppUser user = null;
            if (id != 0)
                user = await GetUserProfileById(id);
            if (user == null)
            {
                await _context.AppUser.AddAsync(new AppUser
                {
                    FirstName = viewModel.FirstName,
                    SecondName = viewModel.SecondName,
                    DateOfBirth = viewModel.DateOfBirth,
                    DateOfRegoistration = DateTime.Now,
                    Key = key,
                    Sex = viewModel.Sex,
                    CurrentWeight = viewModel.CurrentWeight,
                    Goal = viewModel.Goal,
                    WeeksForGoal = viewModel.WeeksForGoal,
                    ImageName = viewModel.ImageName,
                    ImagePath = viewModel.ImagePath,
                    Height = viewModel.Height,
                    Activity = viewModel.Activity
                });
            }
            else
            {
                user.FirstName = viewModel.FirstName;
                user.SecondName = viewModel.SecondName;
                user.DateOfBirth = viewModel.DateOfBirth;
                user.Sex = viewModel.Sex;
                user.CurrentWeight = viewModel.CurrentWeight;
                user.Goal = viewModel.Goal;
                user.WeeksForGoal = viewModel.WeeksForGoal;
                user.ImageName = viewModel.ImageName;
                user.ImagePath = viewModel.ImagePath;
                user.Height = viewModel.Height;
                user.Activity = viewModel.Activity;
            }
            await _context.SaveChangesAsync();
            return await GetUserByKeyAsync(key);
        }

        public async Task<AppUser> GetUserByKeyAsync(string key)
        { var user=await _context.AppUser.Where(k => k.Key.Contains(key)).SingleOrDefaultAsync();
            if (user == default(AppUser))
                return null;
            return user;
        }
    }
}