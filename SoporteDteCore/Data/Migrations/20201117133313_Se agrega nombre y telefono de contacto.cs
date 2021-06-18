using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class Seagreganombreytelefonodecontacto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreContacto",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoContacto",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreContacto",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TelefonoContacto",
                table: "Tickets");
        }
    }
}
