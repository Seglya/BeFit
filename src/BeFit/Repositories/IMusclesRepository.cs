using BeFit.Data;
using BeFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
   public interface IMusclesRepository
    {
        IEnumerable<Muscle> Muscles { get; }
        Task<Muscle> GetMuscleAsync(int? id);
        int? GetIndex(Muscle muscle);
        Muscle GetMuscleByIndex(int index);
        Task<Muscle> SaveMuscleAsync(string name, int id);
        IEnumerable<Muscle> MuscleByFilter(string filter);
        void DeleteMuscle(int id);
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
            if (id == 0)
                return null;
            return await _context.Muscle.Include(s=>s.GroupsOfMuscles).SingleOrDefaultAsync(s=>s.MuscleID==id);
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

        public async Task<Muscle> SaveMuscleAsync(string name, int id = 0)
        {
            if (id == 0)
            {
                await _context.Muscle.AddAsync(new Muscle {Name = name});
                await _context.SaveChangesAsync();
                return await _context.Muscle.SingleOrDefaultAsync(t => t.Name == name);
            }
            _context.Muscle.Update(new Muscle {MuscleID = id, Name = name});
            await _context.SaveChangesAsync();
            return await _context.Muscle.FindAsync(id);
        }

        public IEnumerable<Muscle> MuscleByFilter(string filter)
        {
            if (filter != null)
                return _context.Muscle.Where(s => s.Name.Contains(filter)).AsNoTracking();
            return _context.Muscle.AsNoTracking();
        }
        public void DeleteMuscle(int id)
        {
            var del = _context.Muscle.Find(id);
            if (del != null)
            {
                _context.Muscle.Remove(del);
                _context.SaveChanges();
            }
        }
    }
}
