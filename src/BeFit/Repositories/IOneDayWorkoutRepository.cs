using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace BeFit.Repositories
{
    public interface IOneDayWorkoutRepository
    {
        IEnumerable<OneDayWorkout> AllOneDayWorkoutsByUserId(int userId);
        Task<OneDayWorkout> GetOneDayWorkoutByIdAsync(int id);
        Task<OneDayWorkout> AddOrEditOneDayWorkoutAsync(int id, OneDayWorkoutViewModel viewModel);
        void DeleteOneDayWorkout(int id);
        List<chartItem> ChartData(int id);

    }

    public class OneDayWorkoutRepository : IOneDayWorkoutRepository
    {
        private BeFitDbContext _context;

        public OneDayWorkoutRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OneDayWorkout> AllOneDayWorkoutsByUserId(int userId)
        {
            return
                _context.OneDayWorkout.Include(t => t.Cardio)
                    .Include(t => t.Workout)
                    .ThenInclude(t => t.Exercises)
                    .ThenInclude(t => t.Exercise)
                    .AsNoTracking()
                    .Where(t => t.AppUserID == userId);
        }

        public async Task<OneDayWorkout> GetOneDayWorkoutByIdAsync(int id)
        {
            return await _context.OneDayWorkout.Include(t => t.Cardio)
                    .Include(t => t.Workout)
                    .ThenInclude(t => t.Exercises)
                    .ThenInclude(t => t.Exercise).SingleOrDefaultAsync(t=>t.OneDayWorkoutID==id);
        }

        public async Task<OneDayWorkout> AddOrEditOneDayWorkoutAsync(int id, OneDayWorkoutViewModel viewModel)
        {
            var oneDayWorkout =_context.OneDayWorkout.Find(id);
            if (oneDayWorkout ==null)
            {
                oneDayWorkout=new OneDayWorkout {AppUserID = viewModel.AppUserID,CardioID = viewModel.CardioID,Date = viewModel.Date,Duration = viewModel.Duration,WorkoutID = viewModel.WorkoutID};
                _context.OneDayWorkout.Add(oneDayWorkout);
                _context.SaveChanges();
                return await _context.OneDayWorkout.Include(t => t.Cardio)
                    .Include(t => t.Workout)
                    .ThenInclude(t => t.Exercises)
                    .ThenInclude(t => t.Exercise).LastAsync();
            }

            else
            {
                oneDayWorkout.CardioID = viewModel.CardioID;
                oneDayWorkout.Duration = viewModel.Duration;
                oneDayWorkout.WorkoutID = viewModel.WorkoutID;
                _context.SaveChanges();

            }

            return oneDayWorkout;
        }

        public void DeleteOneDayWorkout(int id)
        {
            var oneDayWorkout = _context.OneDayWorkout.Find(id);
            if (oneDayWorkout != null)
            {
                _context.OneDayWorkout.Remove(oneDayWorkout);
                _context.SaveChanges();
            }
        }

        public List<chartItem> ChartData(int id)
        {
            var weight = _context.AppUser.Find(id).CurrentWeight;
            var coll = _context.OneDayWorkout.OrderBy(t => t.Date).Include(t=>t.Cardio).Where(t => t.AppUserID == id);
            var start = _context.AppUser.Find(id).DateOfRegoistration;
            var max = (DateTime.Today-start).Days;
            var cartItems = new List<chartItem>();
            for (var i = 0; i <= max; i++)
            {
                foreach (var workout in coll)
                {
                    cartItems.Add(workout.Date.Date == start.AddDays(i).Date
                        ? new chartItem
                        {
                            date = start.AddDays(i),
                            visits = workout.Duration *weight* workout.Cardio.CalPerHour/60
                        }
                        : new chartItem {date = start.AddDays(i), visits = 0});
                }
             
            }
            return cartItems;


        }

    }

  
}
