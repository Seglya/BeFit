using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.WorkoutViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IWorkoutRepository
    {
        IEnumerable<Workout> Workouts { get; }
        IEnumerable<Workout> WorkoutsByFilter(string filter);
        Task<Workout> GetWorkoutAsync(int? id);
        int SaveWorkoutAsync(WorkoutViewModel viewModel, int id);
        bool DeleteWorkout(int id);
    }

    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly BeFitDbContext _context;

        public WorkoutRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public bool DeleteWorkout(int id)
        {
            var del = _context.Workout.Find(id);
            if (del != null)
            {
                _context.Workout.Remove(del);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Workout> Workouts => _context.Workout.AsNoTracking();

        public IEnumerable<Workout> WorkoutsByFilter(string filter)
        {
            if (filter != null)
                return
                    _context.Workout.Where(s => s.Name.Contains(filter))
                        .Include(t => t.Exercises)
                        .ThenInclude(e => e.Exercise)
                        .Include(t => t.Tag)
                        .AsNoTracking();
            return
                _context.Workout.Include(t => t.Exercises)
                    .ThenInclude(e => e.Exercise)
                    .Include(t => t.Tag)
                    .AsNoTracking();
        }

        public async Task<Workout> GetWorkoutAsync(int? id)
        {
            return
                await _context.Workout.Include(s => s.Exercises)
                    .ThenInclude(m => m.Exercise).AsNoTracking().Include(t => t.Tag)
                    .SingleOrDefaultAsync(m => m.WorkoutID == id);
        }

        public int SaveWorkoutAsync(WorkoutViewModel viewModel, int id = 0)
        {
            var workout = new Workout();
            if (id != 0)
            {
                workout = _context.Workout.Find(id);
                if (workout == null)
                    return 0;
                if (viewModel.Exercises != null)
                    workout.Exercises = viewModel.Exercises;
            }
            workout.PersonWorkout = viewModel.PersonWorkout;
            workout.Name = viewModel.Name;
            workout.Description = viewModel.Description;
            workout.TagID = viewModel.TagId;
            //workout.Exercises = viewModel.Exercises;

            if (id == 0)
            {
                _context.Workout.Add(workout);
                _context.SaveChanges();

                return _context.Workout.LastAsync().Result.WorkoutID;
            }

            _context.Update(workout);
            _context.SaveChanges();
            return id;
        }
    }
}