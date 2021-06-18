using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombrePersonaRecibe",
                table: "WorkOrders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rut",
                table: "Bloqueoes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazonSocial",
                table: "Bloqueoes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombrePersonaRecibe",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Rut",
                table: "Bloqueoes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RazonSocial",
                table: "Bloqueoes",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
