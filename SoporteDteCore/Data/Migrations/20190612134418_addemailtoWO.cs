using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addemailtoWO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreTecnico",
                table: "WorkOrders",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "NombreImagenFirma",
                table: "WorkOrders",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CorreoElectronico",
                table: "WorkOrders",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorreoElectronico",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "NombreTecnico",
                table: "WorkOrders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreImagenFirma",
                table: "WorkOrders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
