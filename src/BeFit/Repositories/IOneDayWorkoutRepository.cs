using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IOneDayWorkoutRepository
    {
        IEnumerable<OneDayWorkout> AllOneDayWorkoutsByUserId(int userId);
        Task<OneDayWorkout> GetOneDayWorkoutByIdAsync(int id);
        Task<OneDayWorkout> AddOrEditOneDayWorkoutAsync(int id, OneDayWorkoutViewModel viewModel);
        void DeleteOneDayWorkout(int id);

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
            var oneDayWorkout = await GetOneDayWorkoutByIdAsync(id);
            if (oneDayWorkout == default(OneDayWorkout))
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

    }
}
