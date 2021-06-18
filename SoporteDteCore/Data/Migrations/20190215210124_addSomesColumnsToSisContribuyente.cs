using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class addSomesColumnsToSisContribuyente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnServer",
                table: "SisContribuyente",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IsOnline",
                table: "SisContribuyente",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnServer",
                table: "SisContribuyente");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "SisContribuyente");
        }
    }
}
