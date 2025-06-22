using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace keykeeper_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletePoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_Location",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Addresses",
                type: "geometry",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Location",
                table: "Addresses",
                column: "Location")
                .Annotation("Npgsql:IndexMethod", "GIST");
        }
    }
}
