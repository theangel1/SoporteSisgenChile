using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addEnvioCartasTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnvioCartas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaMorosidad = table.Column<DateTime>(nullable: false),
                    FechaEnvioCarta1 = table.Column<DateTime>(nullable: false),
                    FechaEnvioCarta2 = table.Column<DateTime>(nullable: false),
                    FechaEnvioCarta3 = table.Column<DateTime>(nullable: false),
                    Morosidad = table.Column<double>(nullable: false),
                    SinAtraso = table.Column<double>(nullable: false),
                    TotalDeuda = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvioCartas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvioCartas");
        }
    }
}
