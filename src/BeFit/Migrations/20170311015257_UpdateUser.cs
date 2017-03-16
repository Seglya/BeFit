using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeFit.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "AppUser");

            migrationBuilder.RenameColumn(
                name: "ImageMimeType",
                table: "AppUser",
                newName: "Sex");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "AppUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AppUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AppUser");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "AppUser",
                newName: "ImageMimeType");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "AppUser",
                nullable: true);
        }
    }
}
