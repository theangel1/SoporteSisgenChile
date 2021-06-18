using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class AddUserIdOnTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdDerivado",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdDerivado",
                table: "Tickets",
                column: "IdDerivado");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_IdDerivado",
                table: "Tickets",
                column: "IdDerivado",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_IdDerivado",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_IdDerivado",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IdDerivado",
                table: "Tickets");
        }
    }
}
