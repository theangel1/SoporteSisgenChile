using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class Semodificaclasecontrolestado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProcesoTerminado",
                table: "ControlEstado",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "ControlDetalle",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcesoTerminado",
                table: "ControlEstado");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "ControlDetalle");
        }
    }
}
