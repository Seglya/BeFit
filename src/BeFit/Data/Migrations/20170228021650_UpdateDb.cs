using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeFit.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainersServices_ServiceID",
                table: "TrainersServices");

            migrationBuilder.DropIndex(
                name: "IX_GroupsOfMuscles_ExerciseID",
                table: "GroupsOfMuscles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TrainersServices_ServiceID",
                table: "TrainersServices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfMuscles_ExerciseID",
                table: "GroupsOfMuscles",
                column: "ExerciseID");
        }
    }
}
