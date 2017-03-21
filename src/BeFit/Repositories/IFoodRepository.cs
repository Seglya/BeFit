using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.FoodViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IFoodRepository
    {
        IEnumerable<Food> Food { get; }
        Task<Food> SaveFoodAsync(FoodViewModel viewModel, int id);
        IEnumerable<Food> FoodByFilter(string filter);
        void DeleteFood(int id);
        Task<Food> GetFoodAsync(int id);
    }

    public class FoodRepository : IFoodRepository
    {
        private readonly BeFitDbContext _context;

        public FoodRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Food> Food => _context.Foodstuff.AsNoTracking();

        public async Task<Food> SaveFoodAsync(FoodViewModel viewModel, int id = 0)
        {
            if (id == 0)
            {
                await _context.Foodstuff.AddAsync(new Food {Name = viewModel.Name});
                await _context.SaveChangesAsync();
                return await _context.Foodstuff.SingleOrDefaultAsync(t => t.Name == viewModel.Name);
            }
            _context.Foodstuff.Update(new Food
            {
                FoodID = id,
                Name = viewModel.Name,
                Calories = viewModel.Calories,
                Fat = viewModel.Fat,
                Carbohydrate = viewModel.Carbohydrate,
                Protein = viewModel.Protein
            });
            await _context.SaveChangesAsync();
            return await _context.Foodstuff.FindAsync(id);
        }

        public IEnumerable<Food> FoodByFilter(string filter)
        {
            if (filter != null)
                return _context.Foodstuff.Where(s => s.Name.Contains(filter)).AsNoTracking();
            return _context.Foodstuff.AsNoTracking();
        }

        public async Task<Food> GetFoodAsync(int id = 0)
        {
            if (id != 0)
                return await _context.Foodstuff.Include(s => s.WeightsOfFood).SingleOrDefaultAsync(i => i.FoodID == id);
            return null;
        }

        public void DeleteFood(int id)
        {
            var del = _context.Foodstuff.Find(id);
            if (del != null)
            {
                _context.Foodstuff.Remove(del);
                _context.SaveChanges();
            }
        }
    }
}