using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VincKiralamaProjesi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFirmAndQuoteRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CraneId",
                table: "QuoteRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmId",
                table: "QuoteRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Firms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Firms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Firms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Firms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_CraneId",
                table: "QuoteRequests",
                column: "CraneId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_FirmId",
                table: "QuoteRequests",
                column: "FirmId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_Cranes_CraneId",
                table: "QuoteRequests",
                column: "CraneId",
                principalTable: "Cranes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_Firms_FirmId",
                table: "QuoteRequests",
                column: "FirmId",
                principalTable: "Firms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_Cranes_CraneId",
                table: "QuoteRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_Firms_FirmId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_CraneId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_FirmId",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "CraneId",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "FirmId",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Firms");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Firms");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Firms");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Firms");
        }
    }
}
