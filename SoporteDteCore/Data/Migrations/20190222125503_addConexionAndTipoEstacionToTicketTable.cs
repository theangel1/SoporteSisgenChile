using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addConexionAndTipoEstacionToTicketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WorkOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Conexion",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoEstacion",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "Conexion",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TipoEstacion",
                table: "Tickets");
        }
    }
}
