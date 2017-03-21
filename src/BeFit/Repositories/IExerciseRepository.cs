using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.ExerciseViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IExerciseRepository
    {
        IEnumerable<Exercise> Exercises { get; }
        IEnumerable<Exercise> ExercisesByFilter(string filter);
        Task<Exercise> GetExerciseAsync(int? id);
        Exercise GetExercise(string name);
        bool DeleteExercise(int id);
        Task<int> SaveExerciseAsync(CreateExerciseViewModel viewModel, int id);

        //IEnumerable<GroupsOfMuscles> GetMusclesAsync(int id);
    }

    public class ExerciseRepository : IExerciseRepository
    {
        private readonly BeFitDbContext context;

        public ExerciseRepository(BeFitDbContext cont)
        {
            context = cont;
        }


        public IEnumerable<Exercise> Exercises => context.Exercise.AsNoTracking();

        public IEnumerable<Exercise> ExercisesByFilter(string filter)
        {
            if (filter != null)
                return context.Exercise.Where(s => s.Name.Contains(filter)).AsNoTracking();
            return context.Exercise.AsNoTracking();
        }

        public async Task<Exercise> GetExerciseAsync(int? id)
        {
            return
                await context.Exercise.Include(s => s.Muscles)
                    .ThenInclude(m => m.Muscle)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ExerciseID == id);
        }

        public Exercise GetExercise(string name)
        {
            foreach (var ex in context.Exercise)
                if (ex.Name == name)
                    return ex;
            return null;
        }

        public bool DeleteExercise(int id)
        {
            var del = context.Exercise.Find(id);
            if (del != null)
            {
                context.Exercise.Remove(del);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<int> SaveExerciseAsync(CreateExerciseViewModel viewModel, int id = 0)
        {
            var exercise = new Exercise();
            if (id != 0)
            {
                exercise = await GetExerciseAsync(id);
                if (exercise == null)
                    return 0;
                if (viewModel.Muscles != null)
                    exercise.Muscles = viewModel.Muscles;
            }

            exercise.Name = viewModel.Name;
            exercise.Description = viewModel.Description;


            if (viewModel.image != null)
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.image.CopyToAsync(memoryStream);
                    exercise.ImageData = memoryStream.ToArray();

                    exercise.ImageMimeType = viewModel.image.ContentType;
                }

            if (id == 0)
            {
                context.Exercise.Add(exercise);
                context.SaveChanges();

                return GetExercise(exercise.Name).ExerciseID;
            }

            context.Update(exercise);
            context.SaveChanges();
            return id;
        }
    }
}