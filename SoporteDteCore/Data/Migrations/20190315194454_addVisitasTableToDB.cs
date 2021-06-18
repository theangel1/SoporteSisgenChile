using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addVisitasTableToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visita",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RutCliente = table.Column<string>(nullable: true),
                    RazonSocial = table.Column<string>(nullable: true),
                    NumeroContacto = table.Column<string>(nullable: true),
                    NombreContacto = table.Column<string>(nullable: true),
                    MotivoVisita = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    FechaProgramacion = table.Column<DateTime>(nullable: false),
                    EstadoActual = table.Column<string>(nullable: true),
                    EstadoFinal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visita", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visita");
        }
    }
}
