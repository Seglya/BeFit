using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeFit.Migrations
{
    public partial class UpdatingDB : Migration
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

            migrationBuilder.AddColumn<bool>(
                name: "PersonWorkout",
                table: "Workout",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_FillMeasurement_MeasurementID",
                table: "FillMeasurement",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_FillMeasurement_MeasurementOnDateID",
                table: "FillMeasurement",
                column: "MeasurementOnDateID");

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
                name: "IX_WeightOfFood_FoodID",
                table: "WeightOfFood",
                column: "FoodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonWorkout",
                table: "Workout");

            migrationBuilder.DropTable(
                name: "FillMeasurement");

            migrationBuilder.DropTable(
                name: "Ingestion");

            migrationBuilder.DropTable(
                name: "OneDayWorkout");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "MeasurementOnDate");

            migrationBuilder.DropTable(
                name: "OneDayFood");

            migrationBuilder.DropTable(
                name: "WeightOfFood");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
