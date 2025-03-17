using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_MVC.Migrations
{
    /// <inheritdoc />
    public partial class EditAddressColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Students",
                newName: "Email");
        }
    }
}
