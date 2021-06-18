using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class AddControlDetalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlDetalle_ControlEstado_ControlEstadoId",
                table: "ControlDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControlDetalle",
                table: "ControlDetalle");

            migrationBuilder.RenameTable(
                name: "ControlDetalle",
                newName: "ControlDetalles");

            migrationBuilder.RenameIndex(
                name: "IX_ControlDetalle_ControlEstadoId",
                table: "ControlDetalles",
                newName: "IX_ControlDetalles_ControlEstadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControlDetalles",
                table: "ControlDetalles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDetalles_ControlEstado_ControlEstadoId",
                table: "ControlDetalles",
                column: "ControlEstadoId",
                principalTable: "ControlEstado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlDetalles_ControlEstado_ControlEstadoId",
                table: "ControlDetalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControlDetalles",
                table: "ControlDetalles");

            migrationBuilder.RenameTable(
                name: "ControlDetalles",
                newName: "ControlDetalle");

            migrationBuilder.RenameIndex(
                name: "IX_ControlDetalles_ControlEstadoId",
                table: "ControlDetalle",
                newName: "IX_ControlDetalle_ControlEstadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControlDetalle",
                table: "ControlDetalle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDetalle_ControlEstado_ControlEstadoId",
                table: "ControlDetalle",
                column: "ControlEstadoId",
                principalTable: "ControlEstado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
