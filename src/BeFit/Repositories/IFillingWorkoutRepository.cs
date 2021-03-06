﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IFillingWorkoutRepository
    {
        IEnumerable<FillingWorkout> FillingWorkout { get; }
        Task<FillingWorkout> NewFillingWorkoutAsync(int workoutId, Exercise exercise, int sets, int min, int max);
        IEnumerable<FillingWorkout> FillingWorkoutByWorkouts(int exerciseId);
        void DeleteFillingWorkout(ICollection<FillingWorkout> groups);
    }

    public class FillingWorkoutRepository : IFillingWorkoutRepository
    {
        private readonly BeFitDbContext _context;


        public FillingWorkoutRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public void DeleteFillingWorkout(ICollection<FillingWorkout> groups)
        {
            if (groups.Count() != 0)
            {
                _context.FillingWorkout.RemoveRange(groups);
                _context.SaveChanges();
            }
        }


        public async Task<FillingWorkout> NewFillingWorkoutAsync(int workoutId, Exercise exercise, int sets, int min,
            int max)
        {
            var exerciseId = exercise.ExerciseID;

            if (exerciseId > 0)
            {
                var newFillingWorkout = new FillingWorkout
                {
                    ExerciseID = exerciseId,
                    WorkoutID = workoutId,
                    Sets = sets,
                    RepeatMin = min,
                    RepeatMax = max,
                    Exercise = exercise
                };
                if (_context.FillingWorkout.Contains(newFillingWorkout) == false)
                {
                    await _context.FillingWorkout.AddAsync(newFillingWorkout);

                    await _context.SaveChangesAsync();
                    return newFillingWorkout;
                }
            }


            return null;
        }

        public IEnumerable<FillingWorkout> FillingWorkout => _context.FillingWorkout.AsNoTracking();

        public IEnumerable<FillingWorkout> FillingWorkoutByWorkouts(int WorkoutId)
        {
            return _context.FillingWorkout.Where(b => b.WorkoutID == WorkoutId).AsNoTracking();
        }
    }
}