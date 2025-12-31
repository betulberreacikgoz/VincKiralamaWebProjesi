using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VincKiralamaProjesi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFirmForRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Firms",
                newName: "FirmKey");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Firms",
                newName: "District");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Firms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirmKey",
                table: "Firms",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Firms",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Firms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
