using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class AddFormularioToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formulario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RazonSocial = table.Column<string>(nullable: false),
                    NombreFantasia = table.Column<string>(nullable: false),
                    RutEmpresa = table.Column<string>(nullable: false),
                    Giro = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: false),
                    CodigoActividad = table.Column<int>(nullable: false),
                    Correo = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    RutRep = table.Column<string>(nullable: false),
                    SerieCarnet = table.Column<string>(nullable: false),
                    CorreoRep = table.Column<string>(nullable: false),
                    DireccionRep = table.Column<string>(nullable: false),
                    TelefonoRep = table.Column<string>(nullable: false),
                    FotoRep = table.Column<string>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    ProvinciaId = table.Column<int>(nullable: false),
                    ComunaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formulario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formulario_Comuna_ComunaId",
                        column: x => x.ComunaId,
                        principalTable: "Comuna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Formulario_Provincia_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Formulario_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Formulario_ComunaId",
                table: "Formulario",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Formulario_ProvinciaId",
                table: "Formulario",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Formulario_RegionId",
                table: "Formulario",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formulario");
        }
    }
}
