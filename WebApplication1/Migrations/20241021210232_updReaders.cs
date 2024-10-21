using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class updReaders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Readers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Readers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Readers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Readers",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "Id_Readers",
                table: "Readers",
                newName: "Id_Reader");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Year",
                table: "Books",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Readers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Readers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Readers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Readers",
                newName: "ContactInfo");

            migrationBuilder.RenameColumn(
                name: "Id_Reader",
                table: "Readers",
                newName: "Id_Readers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Readers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Readers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
