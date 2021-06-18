using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addEmailsValidacionBoleta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailCertificando",
                table: "BoletaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailFinalizado",
                table: "BoletaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailInstalado",
                table: "BoletaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailSisgenInstalado",
                table: "BoletaElectronica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailSolicitado",
                table: "BoletaElectronica",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailCertificando",
                table: "BoletaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailFinalizado",
                table: "BoletaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailInstalado",
                table: "BoletaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailSisgenInstalado",
                table: "BoletaElectronica");

            migrationBuilder.DropColumn(
                name: "EmailSolicitado",
                table: "BoletaElectronica");
        }
    }
}
