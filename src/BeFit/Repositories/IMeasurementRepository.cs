using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.MeasurementViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IMeasurementRepository
    {

        IEnumerable<Measurement> Measurement { get; }
        Task<Measurement> SaveMeasurementAsync(MeasurementViewModel viewModel, int id);
        IEnumerable<Measurement> MeasurementByFilter(string filter);
        void DeleteMeasurement(int id);
        Task<Measurement> GetMeasurementAsync(int id);
        Task<Measurement> GetMeasurementByNameAsync(string name);
    }

    public class MeasurementRepository : IMeasurementRepository
    {
        private BeFitDbContext _context;

        public MeasurementRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Measurement> Measurement => _context.Measurement.AsNoTracking();

        public async Task<Measurement> SaveMeasurementAsync(MeasurementViewModel viewModel, int id = 0)
        {

         
            if (id == 0 & await _context.Measurement.SingleOrDefaultAsync(t => t.Name == viewModel.Name)==default(Measurement))
            {
                await _context.Measurement.AddAsync(new Measurement { Name = viewModel.Name, UnitsOfMeasurement = viewModel.UnitsOfMeasurement});
                await _context.SaveChangesAsync();
                return await GetMeasurementByNameAsync(viewModel.Name);
            }
            var measure =await GetMeasurementAsync(id);
            measure.Name = viewModel.Name;
            measure.UnitsOfMeasurement = viewModel.UnitsOfMeasurement;
            await _context.SaveChangesAsync();
            return measure;
        }
        public IEnumerable<Measurement> MeasurementByFilter(string filter)
        {
            if (filter != null)
                return _context.Measurement.Where(s => s.Name.Contains(filter)).AsNoTracking();
            return _context.Measurement.AsNoTracking();
        }

        public async Task<Measurement> GetMeasurementAsync(int id = 0)
        {
            if (id != 0)
                return await _context.Measurement.FindAsync(id);
            else
            {
                return null;
            }
        }
        public async Task<Measurement> GetMeasurementByNameAsync(string name)
        {
            if (name != null)
                return await _context.Measurement.SingleOrDefaultAsync(s=>s.Name.Equals(name));
            else
            {
                return null;
            }
        }

        public void DeleteMeasurement(int id)
        {
            var del = GetMeasurementAsync(id);
            if (del.Result != null)
            {
                _context.Measurement.Remove(del.Result);
                _context.SaveChanges();
            }
        }
    }
}
