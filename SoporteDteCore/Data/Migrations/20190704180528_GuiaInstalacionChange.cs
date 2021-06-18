using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class GuiaInstalacionChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletarFormulario",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "ConfigurarRutas",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "DemostracionReporte",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "DescargarCarpeta",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "DescargarFolios",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "EliminarFoliosPrueba",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "HabilitarParametro",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "HacerDeclaracion",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "ParametroBoletaElectronica",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "ReObtenerFolios",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "UbicarCarpeta",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "UbicarEjecutable",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "VerificarParametro",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "VersionWatcher",
                table: "InstalacionBoleta");

            migrationBuilder.AddColumn<string>(
                name: "CheckList",
                table: "InstalacionBoleta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Empresa",
                table: "InstalacionBoleta",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckList",
                table: "InstalacionBoleta");

            migrationBuilder.DropColumn(
                name: "Empresa",
                table: "InstalacionBoleta");

            migrationBuilder.AddColumn<bool>(
                name: "CompletarFormulario",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfigurarRutas",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DemostracionReporte",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DescargarCarpeta",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DescargarFolios",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EliminarFoliosPrueba",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HabilitarParametro",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HacerDeclaracion",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ParametroBoletaElectronica",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReObtenerFolios",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UbicarCarpeta",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UbicarEjecutable",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VerificarParametro",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VersionWatcher",
                table: "InstalacionBoleta",
                nullable: false,
                defaultValue: false);
        }
    }
}
