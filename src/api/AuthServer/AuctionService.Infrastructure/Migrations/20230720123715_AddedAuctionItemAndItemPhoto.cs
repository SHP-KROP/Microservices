using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuctionItemAndItemPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionItem",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimalBid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSellingNow = table.Column<bool>(type: "bit", nullable: false),
                    IsSold = table.Column<bool>(type: "bit", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionItem_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalSchema: "auction",
                        principalTable: "Auctions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuctionItemPhoto",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuctionItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItemPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionItemPhoto_AuctionItem_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalSchema: "auction",
                        principalTable: "AuctionItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItem_AuctionId",
                schema: "auction",
                table: "AuctionItem",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItemPhoto_AuctionItemId",
                schema: "auction",
                table: "AuctionItemPhoto",
                column: "AuctionItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionItemPhoto",
                schema: "auction");

            migrationBuilder.DropTable(
                name: "AuctionItem",
                schema: "auction");
        }
    }
}
