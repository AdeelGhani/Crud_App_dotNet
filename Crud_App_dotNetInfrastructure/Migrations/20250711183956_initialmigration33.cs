using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud_App_dotNetInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Categories",
                newName: "CategoryDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryDescription",
                table: "Categories",
                newName: "Description");
        }
    }
}
