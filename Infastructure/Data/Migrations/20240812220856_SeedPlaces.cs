using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: ["Name"],
                values: new object[,]
                {
                    { "Country A" },
                    { "Country B" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: ["CountryId", "Name"],
                values: new object[,]
                {
                    { 1, "City A1" },
                    { 1, "City A2" },
                    { 2, "City B1" }
                });

            migrationBuilder.InsertData(
                table: "Streetes",
                columns: ["CityId", "Name", "PostalCode"],
                values: new object[,]
                {
                    { 1, "Street A1-1", "12345" },
                    { 1, "Street A1-2", "12346" },
                    { 2, "Street A2-1", "12347" },
                    { 3, "Street B1-1", "12348" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Streetes",
                keyColumn: "Id",
                keyValues: []);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValues: []);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValues: []);
        }
    }
}
