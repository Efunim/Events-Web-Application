using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameStreetesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Streetes",
                newName: "Streets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Streets",
                newName: "Streetes");
        }
    }
}
