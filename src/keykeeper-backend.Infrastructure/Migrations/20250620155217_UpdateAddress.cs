using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace keykeeper_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_SettlementId_StreetId_HouseNumber",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Longitude_Latitude",
                table: "Addresses",
                columns: new[] { "Longitude", "Latitude" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SettlementId",
                table: "Addresses",
                column: "SettlementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_Longitude_Latitude",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_SettlementId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SettlementId_StreetId_HouseNumber",
                table: "Addresses",
                columns: new[] { "SettlementId", "StreetId", "HouseNumber" },
                unique: true,
                filter: "[StreetId] IS NOT NULL AND [HouseNumber] IS NOT NULL");
        }
    }
}
