using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class Number4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaSuspencion",
                table: "Bloqueos",
                newName: "FechaBloqueo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaBloqueo",
                table: "Bloqueos",
                newName: "FechaSuspencion");
        }
    }
}
