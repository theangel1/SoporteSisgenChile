using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class Limpiezadebasededatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvioCartas");

            migrationBuilder.DropTable(
                name: "InstalacionBoleta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnvioCartas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaEnvioCarta1 = table.Column<DateTime>(nullable: true),
                    FechaEnvioCarta2 = table.Column<DateTime>(nullable: true),
                    FechaEnvioCarta3 = table.Column<DateTime>(nullable: true),
                    FechaMorosidad = table.Column<DateTime>(nullable: false),
                    Morosidad = table.Column<double>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: false),
                    RutCliente = table.Column<string>(nullable: false),
                    SinAtraso = table.Column<double>(nullable: false),
                    TotalDeuda = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvioCartas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstalacionBoleta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckList = table.Column<string>(nullable: true),
                    CompletarFormulario = table.Column<bool>(nullable: false),
                    ConfigurarRutas = table.Column<bool>(nullable: false),
                    DemostracionReporte = table.Column<bool>(nullable: false),
                    DescargarCarpeta = table.Column<bool>(nullable: false),
                    DescargarFolios = table.Column<bool>(nullable: false),
                    EliminarFoliosPrueba = table.Column<bool>(nullable: false),
                    Empresa = table.Column<string>(nullable: true),
                    HabilitarParametro = table.Column<bool>(nullable: false),
                    HacerDeclaracion = table.Column<bool>(nullable: false),
                    ParametroBoletaElectronica = table.Column<bool>(nullable: false),
                    ReObtenerFolios = table.Column<bool>(nullable: false),
                    UbicarCarpeta = table.Column<bool>(nullable: false),
                    UbicarEjecutable = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    VerificarParametro = table.Column<bool>(nullable: false),
                    VersionWatcher = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalacionBoleta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstalacionBoleta_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstalacionBoleta_UserId",
                table: "InstalacionBoleta",
                column: "UserId");
        }
    }
}
