using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace locmovie.Migrations
{
    /// <inheritdoc />
    public partial class AddDueDateToRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Rentals",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Rentals",
                newName: "ReturnDate");
        }
    }
}
