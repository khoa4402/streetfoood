using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreetFood.Api.Migrations
{
    /// <inheritdoc />
    public partial class addOwnerToPoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "PointsOfInterest",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "PointsOfInterest");
        }
    }
}
