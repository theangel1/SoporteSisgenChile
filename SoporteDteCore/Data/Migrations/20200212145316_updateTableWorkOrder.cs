using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class updateTableWorkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreEmpresa",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "RutEmpresa",
                table: "WorkOrders");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "WorkOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_EmpresaId",
                table: "WorkOrders",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Empresa_EmpresaId",
                table: "WorkOrders",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Empresa_EmpresaId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_EmpresaId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "WorkOrders");

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpresa",
                table: "WorkOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RutEmpresa",
                table: "WorkOrders",
                nullable: false,
                defaultValue: "");
        }
    }
}
