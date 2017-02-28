using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BeFit.Data;

namespace BeFit.Migrations
{
    [DbContext(typeof(BeFitDbContext))]
    [Migration("20170204192119_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasIndex("ExerciseID");

                    b.HasIndex("MuscleID");

                    b.ToTable("GroupsOfMuscles");
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

            modelBuilder.Entity("BeFit.Models.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<byte[]>("ImageData");

                    b.Property<string>("ImageMimeType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ServiceID");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("BeFit.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountOfVisits")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("DurationMonth");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("TypeService");

                    b.HasKey("SubscriptionID");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("BeFit.Models.Trainer", b =>
                {
                    b.Property<int>("TrainerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discription");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<byte[]>("ImageData");

                    b.Property<string>("ImageMimeType");

                    b.Property<bool?>("PersonalTrainer");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("TrainerID");

                    b.ToTable("Trainer");
                });

            modelBuilder.Entity("BeFit.Models.TrainersServices", b =>
                {
                    b.Property<int>("ServiceID");

                    b.Property<int>("TrainerID");

                    b.HasKey("ServiceID", "TrainerID");

                    b.HasIndex("ServiceID");

                    b.HasIndex("TrainerID");

                    b.ToTable("TrainersServices");
                });

            modelBuilder.Entity("BeFit.Models.Workout", b =>
                {
                    b.Property<int>("WorkoutID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Tag");

                    b.HasKey("WorkoutID");

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

            modelBuilder.Entity("BeFit.Models.GroupsOfMuscles", b =>
                {
                    b.HasOne("BeFit.Models.Exercise", "Exercise")
                        .WithMany("Muscles")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeFit.Models.Muscle", "Muscle")
                        .WithMany()
                        .HasForeignKey("MuscleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeFit.Models.TrainersServices", b =>
                {
                    b.HasOne("BeFit.Models.Service", "Service")
                        .WithMany("Trainers")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeFit.Models.Trainer", "Trainer")
                        .WithMany("Services")
                        .HasForeignKey("TrainerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
