using BeFit.Data;
using BeFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Repositories
{
   public interface IMusclesRepository
    {
        IEnumerable<Muscle> Muscles { get; }
        Task<Muscle> GetMuscleAsync(int? id);
    }
    public class MuscleRepository : IMusclesRepository
    {
        readonly BeFitDbContext _context;
        public MuscleRepository(BeFitDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Muscle> Muscles => _context.Muscle;

        public async Task<Muscle> GetMuscleAsync(int? id)
        {
            return await _context.Muscle.FindAsync(id);
        }
    }
}
