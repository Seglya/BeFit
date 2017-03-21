using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Repositories
{
    public interface IGroupOfMusclesRepository
    {
        IEnumerable<GroupsOfMuscles> GroupsOfMuscles { get; }
        Task<GroupsOfMuscles> NewGroupOfMusclesAsync(int exerciseId, Muscle muscle);
        IEnumerable<GroupsOfMuscles> GroupsOfMusclesByExercise(int exerciseId);
        void DeleteGroupOfMuscle(ICollection<GroupsOfMuscles> groups);
    }

    public class GroupOfMusclesRepository : IGroupOfMusclesRepository
    {
        private readonly BeFitDbContext _context;


        public GroupOfMusclesRepository(BeFitDbContext context)
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


        public async Task<GroupsOfMuscles> NewGroupOfMusclesAsync(int exerciseId, Muscle muscle)
        {
            var muscleId = muscle.MuscleID;

            if (muscleId > 0)
            {
                var newGroupsOfMuscles = new GroupsOfMuscles
                {
                    ExerciseID = exerciseId,
                    MuscleID = muscleId
                };
                if (_context.GroupsOfMuscles.Contains(newGroupsOfMuscles) == false)
                {
                    await _context.GroupsOfMuscles.AddAsync(newGroupsOfMuscles);

                    await _context.SaveChangesAsync();
                    return newGroupsOfMuscles;
                }
            }


            return null;
        }

        public IEnumerable<GroupsOfMuscles> GroupsOfMuscles => _context.GroupsOfMuscles;

        public IEnumerable<GroupsOfMuscles> GroupsOfMusclesByExercise(int exerciseId)
        {
            return _context.GroupsOfMuscles.Where(b => b.ExerciseID == exerciseId);
        }
    }
}