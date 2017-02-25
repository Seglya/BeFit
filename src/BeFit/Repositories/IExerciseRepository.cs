using BeFit.Data;
using BeFit.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BeFit.Models.ExerciseViewModels;


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
    public class ExerciseRepository:IExerciseRepository
    {
       private readonly BeFitDbContext context;
        
        public ExerciseRepository(BeFitDbContext cont)
        {
            context = cont;
            
        }

    
      
        public IEnumerable<Exercise> Exercises => context.Exercise;

        
        public async Task<Exercise> GetExerciseAsync(int? id)
        {
            return await context.Exercise.Include(s=>s.Muscles).AsNoTracking().SingleOrDefaultAsync(m=>m.ExerciseID==id);
        }
        public  Exercise GetExercise(string name)
        {
           foreach(Exercise ex in context.Exercise)
            {
                if(ex.Name == name)
                {
                    return ex;
                }
              
            }
                   return null;
            
        }
        public bool DeleteExercise (int id)
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
      public async Task< int> SaveExerciseAsync(CreateExerciseViewModel viewModel, int id=0)
        {
            Exercise exercise = new Exercise
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
            };
            if (viewModel.image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.image.CopyToAsync(memoryStream);
                    exercise.ImageData = memoryStream.ToArray();

                    exercise.ImageMimeType = viewModel.image.ContentType;
                }
            }
            if (id == 0)
            {
                    context.Exercise.Add(exercise);
                context.SaveChanges();

                    return GetExercise(exercise.Name).ExerciseID;
                }
            else if(await GetExerciseAsync(id)!=null)
            {
                exercise.ExerciseID = id;
                context.Update(exercise);
                context.SaveChanges();
                return id;
            }
            return 0;
        }
       
      
    }
}
