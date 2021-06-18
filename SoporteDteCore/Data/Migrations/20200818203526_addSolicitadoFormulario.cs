using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addSolicitadoFormulario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoRep",
                table: "Formulario");

            migrationBuilder.AddColumn<bool>(
                name: "solicitado",
                table: "Formulario",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "solicitado",
                table: "Formulario");

            migrationBuilder.AddColumn<string>(
                name: "FotoRep",
                table: "Formulario",
                nullable: true);
        }
    }
}
