using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class deleteSiscontribuyenteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SisContribuyente");

            migrationBuilder.CreateTable(
                name: "FacturaElectronica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RutCliente = table.Column<string>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaElectronica", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaElectronica");

            migrationBuilder.CreateTable(
                name: "SisContribuyente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<int>(nullable: false),
                    IsOnServer = table.Column<bool>(nullable: false),
                    IsOnline = table.Column<int>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: true),
                    Rut = table.Column<string>(nullable: true),
                    Sis_contribuyente_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SisContribuyente", x => x.ID);
                });
        }
    }
}
