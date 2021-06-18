﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SoporteDteCore.Data.Migrations
{
    public partial class seagregaprioridad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prioridad",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Tickets");
        }
    }
}
