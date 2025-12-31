using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VincKiralamaProjesi.Migrations
{
    /// <inheritdoc />
    public partial class AddFirmAndLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cranes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cranes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Cranes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Cranes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmId",
                table: "Cranes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Firms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cranes_FirmId",
                table: "Cranes",
                column: "FirmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cranes_Firms_FirmId",
                table: "Cranes",
                column: "FirmId",
                principalTable: "Firms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cranes_Firms_FirmId",
                table: "Cranes");

            migrationBuilder.DropTable(
                name: "Firms");

            migrationBuilder.DropIndex(
                name: "IX_Cranes_FirmId",
                table: "Cranes");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Cranes");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Cranes");

            migrationBuilder.DropColumn(
                name: "FirmId",
                table: "Cranes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cranes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cranes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
