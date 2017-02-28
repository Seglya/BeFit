using System.Collections.Generic;
using System.IO;
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


        public IEnumerable<Exercise> Exercises => context.Exercise;


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
            }

            exercise.Name = viewModel.Name;
            exercise.Description = viewModel.Description;
            if(viewModel.Muscles.Count!=0)
            exercise.Muscles = viewModel.Muscles;

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
            else { 
            context.Update(exercise);
            context.SaveChanges();
            return id;}
           
        }
    }
}
