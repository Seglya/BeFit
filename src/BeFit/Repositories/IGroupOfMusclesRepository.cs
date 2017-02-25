using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Repositories
{
   public interface IGroupOfMusclesRepository
    {
        Task<GroupsOfMuscles> NewGroupOfMusclesAsync(int exerciseId, int muscleId);
        IEnumerable<GroupsOfMuscles> GroupsOfMuscles { get; }
        IEnumerable<GroupsOfMuscles> GroupsOfMusclesByExercise(int exerciseId);
        void DeleteGroupOfMuscle(ICollection<GroupsOfMuscles> groups);

    }

    public class GroupOfMusclesRepository : IGroupOfMusclesRepository
    {
        private BeFitDbContext _context;
      

        public GroupOfMusclesRepository(BeFitDbContext context )
        {
            _context = context;
         
        }

        public void DeleteGroupOfMuscle(ICollection<GroupsOfMuscles> groups)
        {
            if (groups.Count() != 0)
            {
                _context.GroupsOfMuscles.RemoveRange(groups);
                _context.SaveChanges();
              
            }
          
        }

        public async Task<GroupsOfMuscles> NewGroupOfMusclesAsync(int exerciseId, int muscleId)
        {
            GroupsOfMuscles newGroupsOfMuscles = new GroupsOfMuscles
            {

                ExerciseID = exerciseId,
                MuscleID = muscleId
            };
            if (_context.GroupsOfMuscles.Contains(newGroupsOfMuscles) == false)
            {
                await _context.GroupsOfMuscles.AddAsync(newGroupsOfMuscles);
           
            await _context.SaveChangesAsync();
            return newGroupsOfMuscles; }
            return null;
        }
    
        public IEnumerable<GroupsOfMuscles> GroupsOfMuscles => _context.GroupsOfMuscles;

        public IEnumerable<GroupsOfMuscles> GroupsOfMusclesByExercise(int exerciseId)
        {
            return _context.GroupsOfMuscles.Where(b => b.ExerciseID == exerciseId);
        }
    }
    
}
