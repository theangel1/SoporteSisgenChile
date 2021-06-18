using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class Seagreganacionalidadyfechaexpiracioncarnetaform : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FechaExpiracionCarnet",
                table: "Formulario",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nacionalidad",
                table: "Formulario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaExpiracionCarnet",
                table: "Formulario");

            migrationBuilder.DropColumn(
                name: "Nacionalidad",
                table: "Formulario");
        }
    }
}
