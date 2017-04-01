using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IOneDayFoodRepository
    {
        IEnumerable<OneDayFood> OneDayFoodsByUserId(int userId);
        Task<OneDayFood> OneDayFoodByUserIdForDateAsync(int userId, DateTime date);
        Task<OneDayFood> AddOrEditOneDayFoodAsync(OneDayFoodViewModel viewModel, int id);
        void DeleteOneDayFood(int id);
        Task<OneDayFood> GetOneDayFoodBiIdAsync(int id);
    }

    public class OneDayFoodRepository : IOneDayFoodRepository
    {
        private BeFitDbContext _context;

        public OneDayFoodRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OneDayFood> OneDayFoodsByUserId(int userId)
        {
            if (userId!=0)
            return _context.OneDayFood.AsNoTracking().Include(t=>t.Ingestions).ThenInclude(t=>t.WeightOfFood).ThenInclude(t=>t.Food).Where(t => t.AppUserID == userId);
            return null;
        }

        public async Task<OneDayFood> OneDayFoodByUserIdForDateAsync(int userId, DateTime date)
        {
            if (userId != 0)
                return await _context.OneDayFood.SingleOrDefaultAsync(t => t.AppUserID == userId & t.Date.Date == date.Date);
            return null;
        }

        public async Task <OneDayFood> AddOrEditOneDayFoodAsync(OneDayFoodViewModel viewModel, int id)
        {var oneDayFood=new OneDayFood();
            var check = await GetOneDayFoodBiIdAsync(id);
            if(check!=null)oneDayFood=check;
            oneDayFood.Water = viewModel.Water;
            if (oneDayFood.OneDayFoodID==0)
            {oneDayFood.Date = viewModel.Date;
            oneDayFood.AppUserID = viewModel.AppUserID;
                 _context.OneDayFood.Add(oneDayFood);
                _context.SaveChanges();
                return await OneDayFoodByUserIdForDateAsync(viewModel.AppUserID, viewModel.Date);
            }
            _context.SaveChanges();
            return await GetOneDayFoodBiIdAsync(id);

        }

        public void DeleteOneDayFood(int id)
        {
            var del = GetOneDayFoodBiIdAsync(id).Result;
            if (del != null)
            {
                _context.OneDayFood.Remove(del);
                _context.SaveChanges();
            }
        }
        public async Task<OneDayFood> GetOneDayFoodBiIdAsync(int id)
        { 
            return await _context.OneDayFood.Include(t=>t.Ingestions).ThenInclude(t=>t.WeightOfFood).ThenInclude(t=>t.Food).SingleOrDefaultAsync(t=>t.OneDayFoodID==id);}
        
    }
}
