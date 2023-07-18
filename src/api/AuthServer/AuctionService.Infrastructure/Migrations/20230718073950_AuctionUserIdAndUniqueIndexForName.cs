using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AuctionUserIdAndUniqueIndexForName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "auction",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_Name",
                schema: "auction",
                table: "Auctions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Auctions_Name",
                schema: "auction",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "auction",
                table: "Auctions");
        }
    }
}
