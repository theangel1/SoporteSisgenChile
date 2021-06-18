using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class AddInstalacionBoletaToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstalacionBoleta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParametroBoletaElectronica = table.Column<bool>(nullable: false),
                    VersionWatcher = table.Column<bool>(nullable: false),
                    DescargarCarpeta = table.Column<bool>(nullable: false),
                    UbicarCarpeta = table.Column<bool>(nullable: false),
                    UbicarEjecutable = table.Column<bool>(nullable: false),
                    ConfigurarRutas = table.Column<bool>(nullable: false),
                    HabilitarParametro = table.Column<bool>(nullable: false),
                    VerificarParametro = table.Column<bool>(nullable: false),
                    ReObtenerFolios = table.Column<bool>(nullable: false),
                    HacerDeclaracion = table.Column<bool>(nullable: false),
                    EliminarFoliosPrueba = table.Column<bool>(nullable: false),
                    CompletarFormulario = table.Column<bool>(nullable: false),
                    DescargarFolios = table.Column<bool>(nullable: false),
                    DemostracionReporte = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstalacionBoleta");
        }
    }
}
