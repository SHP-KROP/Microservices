using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAuctionStartTimeIndexToDescending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartTime",
                schema: "auction",
                table: "Auctions");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StartTime",
                schema: "auction",
                table: "Auctions",
                column: "StartTime",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartTime",
                schema: "auction",
                table: "Auctions");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StartTime",
                schema: "auction",
                table: "Auctions",
                column: "StartTime");
        }
    }
}
