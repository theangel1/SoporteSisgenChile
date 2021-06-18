using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class SeagreganalatablaEmpresacamposdireccioncomunaciudadcorreostelefonosbloqueo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comuna",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Empresa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Correo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Correo_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefono",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefono", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefono_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Correo_EmpresaId",
                table: "Correo",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefono_EmpresaId",
                table: "Telefono",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Correo");

            migrationBuilder.DropTable(
                name: "Telefono");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Comuna",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Empresa");
        }
    }
}
