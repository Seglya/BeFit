using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeFit.Migrations
{
    public partial class DeleteFitnessCentre1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    AppUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentWeight = table.Column<double>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfRegoistration = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Goal = table.Column<double>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(maxLength: 50, nullable: false),
                    Sex = table.Column<string>(nullable: true),
                    WeeksForGoal = table.Column<int>(nullable: false),
                    Activity = table.Column<double>(nullable: false),
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
                name: "FillMeasurement",
                columns: table => new
                {
                    FillMeasurementID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    MeasurementID = table.Column<int>(nullable: false),
                    PutMesurement = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FillMeasurement", x => x.FillMeasurementID);
                    table.ForeignKey(
                        name: "FK_FillMeasurement_AppUser_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AppUser",
                        principalColumn: "AppUserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FillMeasurement_Measurement_MeasurementID",
                        column: x => x.MeasurementID,
                        principalTable: "Measurement",
                        principalColumn: "MeasurementID",
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
                name: "Ingestion",
                columns: table => new
                {
                    IngestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    OneDayFoodID = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "WeightOfFood",
                columns: table => new
                {
                    WeightOfFoodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodID = table.Column<int>(nullable: false),
                    IngestionID = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_WeightOfFood_Ingestion_IngestionID",
                        column: x => x.IngestionID,
                        principalTable: "Ingestion",
                        principalColumn: "IngestionID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_FillMeasurement_AppUserID",
                table: "FillMeasurement",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FillMeasurement_MeasurementID",
                table: "FillMeasurement",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfMuscles_MuscleID",
                table: "GroupsOfMuscles",
                column: "MuscleID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingestion_OneDayFoodID",
                table: "Ingestion",
                column: "OneDayFoodID");

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
                name: "IX_WeightOfFood_FoodID",
                table: "WeightOfFood",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_WeightOfFood_IngestionID",
                table: "WeightOfFood",
                column: "IngestionID");

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
                name: "News");

            migrationBuilder.DropTable(
                name: "OneDayWorkout");

            migrationBuilder.DropTable(
                name: "WeightOfFood");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Muscle");

            migrationBuilder.DropTable(
                name: "Cardio");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Foodstuff");

            migrationBuilder.DropTable(
                name: "Ingestion");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "OneDayFood");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
