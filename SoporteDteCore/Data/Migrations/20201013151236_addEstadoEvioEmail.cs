using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addEstadoEvioEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailCertificando",
                table: "FacturaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailFinalizado",
                table: "FacturaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailInstalado",
                table: "FacturaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailSisgenInstalado",
                table: "FacturaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailSolicitado",
                table: "FacturaElectronica",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailCertificando",
                table: "FacturaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailFinalizado",
                table: "FacturaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailInstalado",
                table: "FacturaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailSisgenInstalado",
                table: "FacturaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailSolicitado",
                table: "FacturaElectronica");
        }
    }
}
