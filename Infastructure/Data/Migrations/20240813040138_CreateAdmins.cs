using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: ["Name", "Surname", "Email", "Birthday", "PasswordHash", "IsAdmin"],
                values: new object[,]
                {
                    { "admin", "admin", "admin1@gmail.com", DateTime.MinValue, "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", true}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
