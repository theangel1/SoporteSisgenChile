using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class Seeliminacampoprocesofinalizadoencontrolestado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcesoTerminado",
                table: "ControlEstado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProcesoTerminado",
                table: "ControlEstado",
                nullable: false,
                defaultValue: false);
        }
    }
}
