using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class sealiminaasignacionhorario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionSabados");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsignacionSabados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MesId = table.Column<int>(nullable: false),
                    NombreTecnico = table.Column<string>(nullable: false),
                    Sat1 = table.Column<bool>(nullable: false),
                    Sat2 = table.Column<bool>(nullable: false),
                    Sat3 = table.Column<bool>(nullable: false),
                    Sat4 = table.Column<bool>(nullable: false),
                    Sat5 = table.Column<bool>(nullable: false),
                    TecnicoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionSabados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionSabados_Mes_MesId",
                        column: x => x.MesId,
                        principalTable: "Mes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionSabados_AspNetUsers_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionSabados_MesId",
                table: "AsignacionSabados",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionSabados_TecnicoId",
                table: "AsignacionSabados",
                column: "TecnicoId");
        }
    }
}
