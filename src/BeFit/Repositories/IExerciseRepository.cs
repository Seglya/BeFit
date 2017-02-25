using BeFit.Data;
using BeFit.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace BeFit.Repositories
{
   public interface IExerciseRepository
    {
        IEnumerable<Exercise> Exercises { get; }
        Task<Exercise> GetExerciseAsync(int? id);
        Exercise GetExercise(string name);
        bool DeleteExercise(int id);
        bool SaveExercise(Exercise ex);
        Task<Muscle> GetMuscleAsync(int? id);
        //IEnumerable<GroupsOfMuscles> GetMusclesAsync(int id);
    }
    public class ExerciseRepository:IExerciseRepository
    {
       private BeFitDbContext context;
        public ExerciseRepository(BeFitDbContext cont)
        {
            context = cont;
        }
      
        public IEnumerable<Exercise> Exercises
        {
            get { return context.Exercise; }
        }
        public async Task<Muscle> GetMuscleAsync(int? id)
        {
            return await context.Muscle.FindAsync(id);
        }
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
      public  bool SaveExercise(Exercise ex)
        {
            if (ex.ExerciseID == 0)
            {
                    context.Exercise.Add(ex);
                    context.SaveChanges();
                    return true;
                }
            else if(GetExerciseAsync(ex.ExerciseID)!=null)
            {
                context.Update(ex);
                context.SaveChanges();
                return true;
            }
            return false;
        }
       
      
    }
}
