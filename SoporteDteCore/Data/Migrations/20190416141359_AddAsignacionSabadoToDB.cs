using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class AddAsignacionSabadoToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsignacionSabados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreTecnico = table.Column<string>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Sat1 = table.Column<bool>(nullable: true),
                    Sat2 = table.Column<bool>(nullable: true),
                    Sat3 = table.Column<bool>(nullable: true),
                    Sat4 = table.Column<bool>(nullable: true),
                    TecnicoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionSabados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionSabados_AspNetUsers_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionSabados_TecnicoId",
                table: "AsignacionSabados",
                column: "TecnicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionSabados");
        }
    }
}
