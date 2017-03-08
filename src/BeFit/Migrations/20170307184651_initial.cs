using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeFit.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    AppUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfRegoistration = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Goal = table.Column<double>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ImageMimeType = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(maxLength: 50, nullable: false),
                    WeeksForGoal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.AppUserID);
                });

            migrationBuilder.CreateTable(
                name: "Cardio",
                columns: table => new
                {
                    CardioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalPerHour = table.Column<double>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardio", x => x.CardioID);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    ExerciseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ImageMimeType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.ExerciseID);
                });

            migrationBuilder.CreateTable(
                name: "Foodstuff",
                columns: table => new
                {
                    FoodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calories = table.Column<int>(nullable: false),
                    Carbohydrate = table.Column<double>(nullable: false),
                    Fat = table.Column<double>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Protein = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foodstuff", x => x.FoodID);
                });

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    MeasurementID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    UnitsOfMeasurement = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.MeasurementID);
                });

            migrationBuilder.CreateTable(
                name: "Muscle",
                columns: table => new
                {
                    MuscleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscle", x => x.MuscleID);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Path = table.Column<string>(maxLength: 50, nullable: false),
                    Tag = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsID);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ImageMimeType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    SubscriptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountOfVisits = table.Column<string>(maxLength: 20, nullable: false),
                    DurationMonth = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    TypeService = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    TrainerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discription = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ImageMimeType = table.Column<string>(nullable: true),
                    PersonalTrainer = table.Column<bool>(nullable: true),
                    SecondName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.TrainerID);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementOnDate",
                columns: table => new
                {
                    MeasurementOnDateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementOnDate", x => x.MeasurementOnDateID);
                    table.ForeignKey(
                        name: "FK_MeasurementOnDate_AppUser_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AppUser",
                        principalColumn: "AppUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneDayFood",
                columns: table => new
                {
                    OneDayFoodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Water = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneDayFood", x => x.OneDayFoodID);
                    table.ForeignKey(
                        name: "FK_OneDayFood_AppUser_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AppUser",
                        principalColumn: "AppUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightOfFood",
                columns: table => new
                {
                    WeightOfFoodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodID = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightOfFood", x => x.WeightOfFoodID);
                    table.ForeignKey(
                        name: "FK_WeightOfFood_Foodstuff_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Foodstuff",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsOfMuscles",
                columns: table => new
                {
                    ExerciseID = table.Column<int>(nullable: false),
                    MuscleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsOfMuscles", x => new { x.ExerciseID, x.MuscleID });
                    table.ForeignKey(
                        name: "FK_GroupsOfMuscles_Exercise_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "Exercise",
                        principalColumn: "ExerciseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsOfMuscles_Muscle_MuscleID",
                        column: x => x.MuscleID,
                        principalTable: "Muscle",
                        principalColumn: "MuscleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    WorkoutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PersonWorkout = table.Column<bool>(nullable: false),
                    TagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.WorkoutID);
                    table.ForeignKey(
                        name: "FK_Workout_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainersServices",
                columns: table => new
                {
                    ServiceID = table.Column<int>(nullable: false),
                    TrainerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainersServices", x => new { x.ServiceID, x.TrainerID });
                    table.ForeignKey(
                        name: "FK_TrainersServices_Service_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Service",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainersServices_Trainer_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "Trainer",
                        principalColumn: "TrainerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FillMeasurement",
                columns: table => new
                {
                    FillMeasurementID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeasurementID = table.Column<int>(nullable: false),
                    MeasurementOnDateID = table.Column<int>(nullable: false),
                    PutMesurement = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FillMeasurement", x => x.FillMeasurementID);
                    table.ForeignKey(
                        name: "FK_FillMeasurement_Measurement_MeasurementID",
                        column: x => x.MeasurementID,
                        principalTable: "Measurement",
                        principalColumn: "MeasurementID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FillMeasurement_MeasurementOnDate_MeasurementOnDateID",
                        column: x => x.MeasurementOnDateID,
                        principalTable: "MeasurementOnDate",
                        principalColumn: "MeasurementOnDateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingestion",
                columns: table => new
                {
                    IngestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    OneDayFoodID = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    WeightOfFoodID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingestion", x => x.IngestionID);
                    table.ForeignKey(
                        name: "FK_Ingestion_OneDayFood_OneDayFoodID",
                        column: x => x.OneDayFoodID,
                        principalTable: "OneDayFood",
                        principalColumn: "OneDayFoodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingestion_WeightOfFood_WeightOfFoodID",
                        column: x => x.WeightOfFoodID,
                        principalTable: "WeightOfFood",
                        principalColumn: "WeightOfFoodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FillingWorkout",
                columns: table => new
                {
                    FillingWorkoutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExerciseID = table.Column<int>(nullable: false),
                    RepeatMax = table.Column<int>(nullable: false),
                    RepeatMin = table.Column<int>(nullable: false),
                    Sets = table.Column<int>(nullable: false),
                    WorkoutID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FillingWorkout", x => x.FillingWorkoutID);
                    table.ForeignKey(
                        name: "FK_FillingWorkout_Exercise_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "Exercise",
                        principalColumn: "ExerciseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FillingWorkout_Workout_WorkoutID",
                        column: x => x.WorkoutID,
                        principalTable: "Workout",
                        principalColumn: "WorkoutID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneDayWorkout",
                columns: table => new
                {
                    OneDayWorkoutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserID = table.Column<int>(nullable: false),
                    CardioID = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    WorkoutID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneDayWorkout", x => x.OneDayWorkoutID);
                    table.ForeignKey(
                        name: "FK_OneDayWorkout_AppUser_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AppUser",
                        principalColumn: "AppUserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OneDayWorkout_Cardio_CardioID",
                        column: x => x.CardioID,
                        principalTable: "Cardio",
                        principalColumn: "CardioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OneDayWorkout_Workout_WorkoutID",
                        column: x => x.WorkoutID,
                        principalTable: "Workout",
                        principalColumn: "WorkoutID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FillingWorkout_ExerciseID",
                table: "FillingWorkout",
                column: "ExerciseID");

            migrationBuilder.CreateIndex(
                name: "IX_FillingWorkout_WorkoutID",
                table: "FillingWorkout",
                column: "WorkoutID");

            migrationBuilder.CreateIndex(
                name: "IX_FillMeasurement_MeasurementID",
                table: "FillMeasurement",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_FillMeasurement_MeasurementOnDateID",
                table: "FillMeasurement",
                column: "MeasurementOnDateID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfMuscles_MuscleID",
                table: "GroupsOfMuscles",
                column: "MuscleID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingestion_OneDayFoodID",
                table: "Ingestion",
                column: "OneDayFoodID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingestion_WeightOfFoodID",
                table: "Ingestion",
                column: "WeightOfFoodID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementOnDate_AppUserID",
                table: "MeasurementOnDate",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OneDayFood_AppUserID",
                table: "OneDayFood",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OneDayWorkout_AppUserID",
                table: "OneDayWorkout",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OneDayWorkout_CardioID",
                table: "OneDayWorkout",
                column: "CardioID");

            migrationBuilder.CreateIndex(
                name: "IX_OneDayWorkout_WorkoutID",
                table: "OneDayWorkout",
                column: "WorkoutID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainersServices_TrainerID",
                table: "TrainersServices",
                column: "TrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_WeightOfFood_FoodID",
                table: "WeightOfFood",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_TagID",
                table: "Workout",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FillingWorkout");

            migrationBuilder.DropTable(
                name: "FillMeasurement");

            migrationBuilder.DropTable(
                name: "GroupsOfMuscles");

            migrationBuilder.DropTable(
                name: "Ingestion");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OneDayWorkout");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "TrainersServices");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "MeasurementOnDate");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Muscle");

            migrationBuilder.DropTable(
                name: "OneDayFood");

            migrationBuilder.DropTable(
                name: "WeightOfFood");

            migrationBuilder.DropTable(
                name: "Cardio");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Foodstuff");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
