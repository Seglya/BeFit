using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IIngestionRepository
    {
        IEnumerable<Ingestion> IngestionsForOneDay(int oneDayId);
        Task<Ingestion> GetIngestionByIdAsync(int id);
        Task<Ingestion> CreateIngestionsAsync(IngestionViewModel viewModel);
        //ICollection<Ingestion> NewCollectionIngection { get; }
    }

    public class IngestionRepository : IIngestionRepository
    {
        private BeFitDbContext _context;

        public IngestionRepository(BeFitDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Ingestion> IngestionsForOneDay(int oneDayId)
        {
            if (oneDayId > 0)
            {
                return _context.Ingestion.AsNoTracking().Where(t => t.OneDayFoodID == oneDayId);
            }
            return null;
        }
        //public ICollection<Ingestion> NewCollectionIngection =>new Collection<Ingestion>
        //{new Ingestion {Name = }
            
        //}
        public async Task<Ingestion> GetIngestionByIdAsync(int id)
        {
            return await _context.Ingestion.Include(t=>t.WeightOfFood).SingleOrDefaultAsync(t=>t.IngestionID==id);
            
        }

        public async Task<Ingestion> CreateIngestionsAsync(IngestionViewModel viewModel)
        {
            var newIng = new Ingestion
            {
                Name = viewModel.Name,
                OneDayFoodID = viewModel.OneDayFoodId
            };
            _context.Ingestion.Add(newIng);
            _context.SaveChanges();
            return await 
                _context.Ingestion.SingleOrDefaultAsync(
                    t => t.Name == viewModel.Name & t.OneDayFoodID == viewModel.OneDayFoodId);
        }

    }

}
