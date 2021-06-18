using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addWorkOrdersTableaToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RutCliente",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEmpresa = table.Column<string>(nullable: false),
                    RutEmpresa = table.Column<string>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    HoraLlegada = table.Column<string>(nullable: true),
                    HoraSalida = table.Column<string>(nullable: true),
                    MotivoVisita = table.Column<string>(nullable: true),
                    TrabajoRealizado = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    NombreTecnico = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "RutCliente",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
