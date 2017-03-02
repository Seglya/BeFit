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
        int? GetIndex(Muscle muscle);
        Muscle GetMuscleByIndex(int index);
    }
    public class MuscleRepository : IMusclesRepository
    {
        readonly BeFitDbContext _context;
        public MuscleRepository(BeFitDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Muscle> Muscles => _context.Muscle;

        public Muscle GetMuscleByIndex(int index)
        {
            
            
            int i = 0;
            foreach (var muscle in _context.Muscle)
            {
                if (i == index)
                   return muscle;
                i++;
            }
            return null;
        }

        public async Task<Muscle> GetMuscleAsync(int? id)
        {
            return await _context.Muscle.FindAsync(id);
        }
        public int? GetIndex(Muscle muscle)
        {
            int i = 0;
            foreach (var musc in _context.Muscle)
            {
                if (muscle.MuscleID == musc.MuscleID)
                    return i;
                i++;
            }
            return null;
        }
    }
}
