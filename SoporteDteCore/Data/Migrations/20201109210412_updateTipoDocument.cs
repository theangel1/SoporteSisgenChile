using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class updateTipoDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoDocumento",
                table: "ControlEstado");

            migrationBuilder.AddColumn<int>(
                name: "TipoServicio",
                table: "ControlEstado",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoServicio",
                table: "ControlEstado");

            migrationBuilder.AddColumn<int>(
                name: "TipoDocumento",
                table: "ControlEstado",
                nullable: false,
                defaultValue: 0);
        }
    }
}
