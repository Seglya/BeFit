using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeFit.Models;
using System.Data;

namespace BeFit.Data
{
    public class BeFitDbContext: DbContext
    {
        public BeFitDbContext(DbContextOptions<BeFitDbContext> options)
            : base(options)
        {
           
                //<BefitDbContext>(new DbInitializer());
        }
       
        public DbSet<Workout> Workout { get; set; }
      
        public DbSet<News> News { get; set; }
        public DbSet<Food> Foodstuff { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Cardio> Cardio { get; set; }
        public DbSet<FillingWorkout> FillingWorkout { get; set; }
        public DbSet<GroupsOfMuscles> GroupsOfMuscles { get; set; }
        public DbSet<Muscle> Muscle { get; set; }
     
        public DbSet<AppUser> AppUser{ get; set; }
        public DbSet<FillMeasurement> FillMeasurement { get; set; }
        public DbSet<Ingestion> Ingestion { get; set; }
        public DbSet<Measurement> Measurement { get; set; }
       
        public DbSet<OneDayFood> OneDayFood { get; set; }
        public DbSet<OneDayWorkout> OneDayWorkout { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<WeightOfFood> WeightOfFood { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("AppUser");
            builder.Entity<Cardio>().ToTable("Cardio");
            builder.Entity<Exercise>().ToTable("Exercise");
            builder.Entity<FillingWorkout>().ToTable("FillingWorkout");
            builder.Entity<FillMeasurement>().ToTable("FillMeasurement");
            builder.Entity<Food>().ToTable("Foodstuff");
            builder.Entity<GroupsOfMuscles>().ToTable("GroupsOfMuscles");
            builder.Entity<Ingestion>().ToTable("Ingestion");
            builder.Entity<Muscle>().ToTable("Muscle");
            builder.Entity<Measurement>().ToTable("Measurement");
           
            builder.Entity<News>().ToTable("News");
            builder.Entity<OneDayFood>().ToTable("OneDayFood");
            builder.Entity<OneDayWorkout>().ToTable("OneDayWorkout");
          
            builder.Entity<Tag>().ToTable("Tag");
           
            builder.Entity<WeightOfFood>().ToTable("WeightOfFood");
            builder.Entity<Workout>().ToTable("Workout");
          
            builder.Entity<GroupsOfMuscles>().HasKey(c => new { c.ExerciseID, c.MuscleID });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
      
    }
}
