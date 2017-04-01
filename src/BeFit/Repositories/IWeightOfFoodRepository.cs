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
    public interface IWeightOfFoodRepository
    {
        IEnumerable<WeightOfFood> WeightOfFoodsByIngectionId(int ingId);
        Task<WeightOfFood> GetWeightOfFoodByIdAsync(int id);
        Task<WeightOfFood> CreatetWeightOfFoodAsync(WeightOfFoodViewModel viewModel);
        void DeleteWeightOfFoodByIngestId(int id);
    }

    public class WeightOfFoodRepository:IWeightOfFoodRepository
    {
        private BeFitDbContext _context;

        public WeightOfFoodRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WeightOfFood> WeightOfFoodsByIngectionId(int ingId)
        {
            if (ingId > 0)
            {
                return _context.WeightOfFood.AsNoTracking().Where(t => t.IngestionID == ingId);
            }
            return null;
        }

        public async Task<WeightOfFood> GetWeightOfFoodByIdAsync(int id)
        {
            return await _context.WeightOfFood.FindAsync(id);
        }

        public async Task<WeightOfFood> CreatetWeightOfFoodAsync(WeightOfFoodViewModel viewModel)
        {
           
               var newWeight=new WeightOfFood();
            newWeight.FoodID = viewModel.Food.FoodID;
            newWeight.Weight = viewModel.Weight;
            if (newWeight.WeightOfFoodID == 0)
            {
                newWeight.IngestionID = viewModel.IngestionId;
                _context.WeightOfFood.Add(newWeight);
                _context.SaveChanges();
                return
                    await _context.WeightOfFood.SingleOrDefaultAsync(
                        t => t.FoodID == viewModel.Food.FoodID & t.IngestionID == viewModel.IngestionId);
            }
            _context.SaveChanges();
            return newWeight;

        }

        public void DeleteWeightOfFoodByIngestId(int injestId)
        {
            var del = WeightOfFoodsByIngectionId(injestId);
            if (del != null)
            {
                _context.WeightOfFood.RemoveRange(del);
                _context.SaveChanges();
            }
        }
    }
}
