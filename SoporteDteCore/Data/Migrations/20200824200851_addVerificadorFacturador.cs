using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addVerificadorFacturador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CambioFacturador",
                table: "BoletaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Facturador",
                table: "BoletaElectronica",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CambioFacturador",
                table: "BoletaElectronica");

            migrationBuilder.DropColumn(
                name: "Facturador",
                table: "BoletaElectronica");
        }
    }
}
