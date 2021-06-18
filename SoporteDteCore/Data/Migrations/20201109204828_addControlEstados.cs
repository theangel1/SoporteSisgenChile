using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addControlEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ControlEstado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RutCliente = table.Column<string>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: false),
                    TipoDocumento = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlEstado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControlDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Usuario = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    ControlEstadoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlDetalle_ControlEstado_ControlEstadoId",
                        column: x => x.ControlEstadoId,
                        principalTable: "ControlEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlDetalle_ControlEstadoId",
                table: "ControlDetalle",
                column: "ControlEstadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlDetalle");

            migrationBuilder.DropTable(
                name: "ControlEstado");

            migrationBuilder.CreateTable(
                name: "ObservacionesFactura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Detalle = table.Column<string>(nullable: true),
                    FacturaElecId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Usuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacionesFactura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacionesFactura_FacturaElectronica_FacturaElecId",
                        column: x => x.FacturaElecId,
                        principalTable: "FacturaElectronica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObservacionesFactura_FacturaElecId",
                table: "ObservacionesFactura",
                column: "FacturaElecId");
        }
    }
}
