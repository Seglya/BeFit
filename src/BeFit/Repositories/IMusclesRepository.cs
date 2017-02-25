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
    }
    public class MuscleRepository : IMusclesRepository
    {
        BeFitDbContext _context;
        public MuscleRepository(BeFitDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Muscle> Muscles { get { return _context.Muscle; } }
    }
}
