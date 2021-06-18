using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class ChangeToMesAddFKToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mes",
                table: "AsignacionSabados",
                newName: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionSabados_MesId",
                table: "AsignacionSabados",
                column: "MesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionSabados_Mes_MesId",
                table: "AsignacionSabados",
                column: "MesId",
                principalTable: "Mes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionSabados_Mes_MesId",
                table: "AsignacionSabados");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionSabados_MesId",
                table: "AsignacionSabados");

            migrationBuilder.RenameColumn(
                name: "MesId",
                table: "AsignacionSabados",
                newName: "Mes");
        }
    }
}
