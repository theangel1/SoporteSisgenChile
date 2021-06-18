using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addEmailAndTelefonoFieldsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FacturaElectronica",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "FacturaElectronica",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BoletaElectronica",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "BoletaElectronica",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "FacturaElectronica");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "FacturaElectronica");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BoletaElectronica");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "BoletaElectronica");
        }
    }
}
