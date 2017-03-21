using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeFit.Migrations
{
    public partial class deleteMeasurementOnDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FillMeasurement_MeasurementOnDate_MeasurementOnDateID",
                table: "FillMeasurement");

            migrationBuilder.DropTable(
                name: "MeasurementOnDate");

            migrationBuilder.RenameColumn(
                name: "MeasurementOnDateID",
                table: "FillMeasurement",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_FillMeasurement_MeasurementOnDateID",
                table: "FillMeasurement",
                newName: "IX_FillMeasurement_AppUserID");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "FillMeasurement",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_FillMeasurement_AppUser_AppUserID",
                table: "FillMeasurement",
                column: "AppUserID",
                principalTable: "AppUser",
                principalColumn: "AppUserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FillMeasurement_AppUser_AppUserID",
                table: "FillMeasurement");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FillMeasurement");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "FillMeasurement",
                newName: "MeasurementOnDateID");

            migrationBuilder.RenameIndex(
                name: "IX_FillMeasurement_AppUserID",
                table: "FillMeasurement",
                newName: "IX_FillMeasurement_MeasurementOnDateID");

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

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementOnDate_AppUserID",
                table: "MeasurementOnDate",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_FillMeasurement_MeasurementOnDate_MeasurementOnDateID",
                table: "FillMeasurement",
                column: "MeasurementOnDateID",
                principalTable: "MeasurementOnDate",
                principalColumn: "MeasurementOnDateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
