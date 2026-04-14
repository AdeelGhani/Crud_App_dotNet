using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud_App_dotNetInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTenatSport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Marrige",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Journals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "JournalDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "EmployeeType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Designation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Marrige");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "JournalDetails");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Designation");
        }
    }
}
