using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BeFit.Data;

namespace BeFit.Migrations
{
    [DbContext(typeof(BeFitDbContext))]
    partial class BeFitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeFit.Models.AppUser", b =>
                {
                    b.Property<int>("AppUserID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrentWeight");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<DateTime>("DateOfRegoistration");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<double>("Goal");

                    b.Property<string>("ImageName");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Key");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Sex");

                    b.Property<int>("WeeksForGoal");

                    b.HasKey("AppUserID");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("BeFit.Models.Cardio", b =>
                {
                    b.Property<int>("CardioID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CalPerHour");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("CardioID");

                    b.ToTable("Cardio");
                });

            modelBuilder.Entity("BeFit.Models.Exercise", b =>
                {
                    b.Property<int>("ExerciseID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<byte[]>("ImageData");

                    b.Property<string>("ImageMimeType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ExerciseID");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("BeFit.Models.FillingWorkout", b =>
                {
                    b.Property<int>("FillingWorkoutID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExerciseID");

                    b.Property<int>("RepeatMax");

                    b.Property<int>("RepeatMin");

                    b.Property<int>("Sets");

                    b.Property<int>("WorkoutID");

                    b.HasKey("FillingWorkoutID");

                    b.HasIndex("ExerciseID");

                    b.HasIndex("WorkoutID");

                    b.ToTable("FillingWorkout");
                });

            modelBuilder.Entity("BeFit.Models.FillMeasurement", b =>
                {
                    b.Property<int>("FillMeasurementID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppUserID");

                    b.Property<DateTime>("Date");

                    b.Property<int>("MeasurementID");

                    b.Property<double>("PutMesurement");

                    b.HasKey("FillMeasurementID");

                    b.HasIndex("AppUserID");

                    b.HasIndex("MeasurementID");

                    b.ToTable("FillMeasurement");
                });

            modelBuilder.Entity("BeFit.Models.Food", b =>
                {
                    b.Property<int>("FoodID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Calories");

                    b.Property<double>("Carbohydrate");

                    b.Property<double>("Fat");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<double>("Protein");

                    b.HasKey("FoodID");

                    b.ToTable("Foodstuff");
                });

            modelBuilder.Entity("BeFit.Models.GroupsOfMuscles", b =>
                {
                    b.Property<int>("ExerciseID");

                    b.Property<int>("MuscleID");

                    b.HasKey("ExerciseID", "MuscleID");

                    b.HasIndex("MuscleID");

                    b.ToTable("GroupsOfMuscles");
                });

            modelBuilder.Entity("BeFit.Models.Ingestion", b =>
                {
                    b.Property<int>("IngestionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("OneDayFoodID");

                    b.HasKey("IngestionID");

                    b.HasIndex("OneDayFoodID");

                    b.ToTable("Ingestion");
                });

            modelBuilder.Entity("BeFit.Models.Measurement", b =>
                {
                    b.Property<int>("MeasurementID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UnitsOfMeasurement")
                        .IsRequired();

                    b.HasKey("MeasurementID");

                    b.ToTable("Measurement");
                });

            modelBuilder.Entity("BeFit.Models.Muscle", b =>
                {
                    b.Property<int>("MuscleID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("MuscleID");

                    b.ToTable("Muscle");
                });

            modelBuilder.Entity("BeFit.Models.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Tag")
                        .IsRequired();

                    b.HasKey("NewsID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("BeFit.Models.OneDayFood", b =>
                {
                    b.Property<int>("OneDayFoodID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppUserID");

                    b.Property<DateTime>("Date");

                    b.Property<double>("Water");

                    b.HasKey("OneDayFoodID");

                    b.HasIndex("AppUserID");

                    b.ToTable("OneDayFood");
                });

            modelBuilder.Entity("BeFit.Models.OneDayWorkout", b =>
                {
                    b.Property<int>("OneDayWorkoutID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppUserID");

                    b.Property<int?>("CardioID");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Duration");

                    b.Property<int?>("WorkoutID");

                    b.HasKey("OneDayWorkoutID");

                    b.HasIndex("AppUserID");

                    b.HasIndex("CardioID");

                    b.HasIndex("WorkoutID");

                    b.ToTable("OneDayWorkout");
                });

            modelBuilder.Entity("BeFit.Models.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.HasKey("TagID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BeFit.Models.WeightOfFood", b =>
                {
                    b.Property<int>("WeightOfFoodID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FoodID");

                    b.Property<int>("IngestionID");

                    b.Property<double>("Weight");

                    b.HasKey("WeightOfFoodID");

                    b.HasIndex("FoodID");

                    b.HasIndex("IngestionID");

                    b.ToTable("WeightOfFood");
                });

            modelBuilder.Entity("BeFit.Models.Workout", b =>
                {
                    b.Property<int>("WorkoutID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("PersonWorkout");

                    b.Property<int>("TagID");

                    b.HasKey("WorkoutID");

                    b.HasIndex("TagID");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("BeFit.Models.FillingWorkout", b =>
                {
                    b.HasOne("BeFit.Models.Exercise", "Exercise")
                        .WithMany("Workouts")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeFit.Models.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeFit.Models.FillMeasurement", b =>
                {
                    b.HasOne("BeFit.Models.AppUser", "AppUser")
                        .WithMany("Measurements")
                        .HasForeignKey("AppUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeFit.Models.Measurement", "Measurement")
                        .WithMany()
                        .HasForeignKey("MeasurementID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeFit.Models.GroupsOfMuscles", b =>
                {
                    b.HasOne("BeFit.Models.Exercise", "Exercise")
                        .WithMany("Muscles")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeFit.Models.Muscle", "Muscle")
                        .WithMany("GroupsOfMuscles")
                        .HasForeignKey("MuscleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeFit.Models.Ingestion", b =>
                {
                    b.HasOne("BeFit.Models.OneDayFood", "OneDayFood")
                        .WithMany("Ingestions")
                        .HasForeignKey("OneDayFoodID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeFit.Models.OneDayFood", b =>
                {
                    b.HasOne("BeFit.Models.AppUser", "AppUser")
                        .WithMany("Food")
                        .HasForeignKey("AppUserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeFit.Models.OneDayWorkout", b =>
                {
                    b.HasOne("BeFit.Models.AppUser", "AppUser")
                        .WithMany("Workouts")
                        .HasForeignKey("AppUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeFit.Models.Cardio", "Cardio")
                        .WithMany("CardioWorkouts")
                        .HasForeignKey("CardioID");

                    b.HasOne("BeFit.Models.Workout", "Workout")
                        .WithMany("OneDayWorkouts")
                        .HasForeignKey("WorkoutID");
                });

            modelBuilder.Entity("BeFit.Models.WeightOfFood", b =>
                {
                    b.HasOne("BeFit.Models.Food", "Food")
                        .WithMany("WeightsOfFood")
                        .HasForeignKey("FoodID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeFit.Models.Ingestion", "Ingestion")
                        .WithMany("WeightOfFood")
                        .HasForeignKey("IngestionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeFit.Models.Workout", b =>
                {
                    b.HasOne("BeFit.Models.Tag", "Tag")
                        .WithMany("Workouts")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
