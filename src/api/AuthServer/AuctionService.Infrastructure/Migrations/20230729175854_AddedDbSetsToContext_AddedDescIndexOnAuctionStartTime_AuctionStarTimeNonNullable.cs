using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbSetsToContext_AddedDescIndexOnAuctionStartTime_AuctionStarTimeNonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItem_Auctions_AuctionId",
                schema: "auction",
                table: "AuctionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItemPhoto_AuctionItem_AuctionItemId",
                schema: "auction",
                table: "AuctionItemPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_AuctionItem_AuctionItemId",
                schema: "auction",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                schema: "auction",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionItemPhoto",
                schema: "auction",
                table: "AuctionItemPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionItem",
                schema: "auction",
                table: "AuctionItem");

            migrationBuilder.RenameTable(
                name: "Bid",
                schema: "auction",
                newName: "Bids",
                newSchema: "auction");

            migrationBuilder.RenameTable(
                name: "AuctionItemPhoto",
                schema: "auction",
                newName: "AuctionItemPhotos",
                newSchema: "auction");

            migrationBuilder.RenameTable(
                name: "AuctionItem",
                schema: "auction",
                newName: "AuctionItems",
                newSchema: "auction");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AuctionItemId",
                schema: "auction",
                table: "Bids",
                newName: "IX_Bids_AuctionItemId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionItemPhoto_AuctionItemId",
                schema: "auction",
                table: "AuctionItemPhotos",
                newName: "IX_AuctionItemPhotos_AuctionItemId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionItem_AuctionId",
                schema: "auction",
                table: "AuctionItems",
                newName: "IX_AuctionItems_AuctionId");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartTime",
                schema: "auction",
                table: "Auctions",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                schema: "auction",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionItemPhotos",
                schema: "auction",
                table: "AuctionItemPhotos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionItems",
                schema: "auction",
                table: "AuctionItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StartTime",
                schema: "auction",
                table: "Auctions",
                column: "StartTime",
                descending: new bool[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItemPhotos_AuctionItems_AuctionItemId",
                schema: "auction",
                table: "AuctionItemPhotos",
                column: "AuctionItemId",
                principalSchema: "auction",
                principalTable: "AuctionItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItems_Auctions_AuctionId",
                schema: "auction",
                table: "AuctionItems",
                column: "AuctionId",
                principalSchema: "auction",
                principalTable: "Auctions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_AuctionItems_AuctionItemId",
                schema: "auction",
                table: "Bids",
                column: "AuctionItemId",
                principalSchema: "auction",
                principalTable: "AuctionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItemPhotos_AuctionItems_AuctionItemId",
                schema: "auction",
                table: "AuctionItemPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItems_Auctions_AuctionId",
                schema: "auction",
                table: "AuctionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_AuctionItems_AuctionItemId",
                schema: "auction",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StartTime",
                schema: "auction",
                table: "Auctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                schema: "auction",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionItems",
                schema: "auction",
                table: "AuctionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionItemPhotos",
                schema: "auction",
                table: "AuctionItemPhotos");

            migrationBuilder.RenameTable(
                name: "Bids",
                schema: "auction",
                newName: "Bid",
                newSchema: "auction");

            migrationBuilder.RenameTable(
                name: "AuctionItems",
                schema: "auction",
                newName: "AuctionItem",
                newSchema: "auction");

            migrationBuilder.RenameTable(
                name: "AuctionItemPhotos",
                schema: "auction",
                newName: "AuctionItemPhoto",
                newSchema: "auction");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionItemId",
                schema: "auction",
                table: "Bid",
                newName: "IX_Bid_AuctionItemId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionItems_AuctionId",
                schema: "auction",
                table: "AuctionItem",
                newName: "IX_AuctionItem_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionItemPhotos_AuctionItemId",
                schema: "auction",
                table: "AuctionItemPhoto",
                newName: "IX_AuctionItemPhoto_AuctionItemId");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartTime",
                schema: "auction",
                table: "Auctions",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                schema: "auction",
                table: "Bid",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionItem",
                schema: "auction",
                table: "AuctionItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionItemPhoto",
                schema: "auction",
                table: "AuctionItemPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItem_Auctions_AuctionId",
                schema: "auction",
                table: "AuctionItem",
                column: "AuctionId",
                principalSchema: "auction",
                principalTable: "Auctions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItemPhoto_AuctionItem_AuctionItemId",
                schema: "auction",
                table: "AuctionItemPhoto",
                column: "AuctionItemId",
                principalSchema: "auction",
                principalTable: "AuctionItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_AuctionItem_AuctionItemId",
                schema: "auction",
                table: "Bid",
                column: "AuctionItemId",
                principalSchema: "auction",
                principalTable: "AuctionItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
