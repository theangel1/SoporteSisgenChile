using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class AddVisitaIdFKtoWorkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitaId",
                table: "WorkOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_VisitaId",
                table: "WorkOrders",
                column: "VisitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Visita_VisitaId",
                table: "WorkOrders",
                column: "VisitaId",
                principalTable: "Visita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Visita_VisitaId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_VisitaId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "VisitaId",
                table: "WorkOrders");
        }
    }
}
