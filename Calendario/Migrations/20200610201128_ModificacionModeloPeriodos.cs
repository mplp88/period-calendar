using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendario.Migrations
{
    public partial class ModificacionModeloPeriodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoximaFechaPosible",
                table: "Periodos");

            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "Periodos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProximaFechaPosible",
                table: "Periodos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Periodos");

            migrationBuilder.DropColumn(
                name: "ProximaFechaPosible",
                table: "Periodos");

            migrationBuilder.AddColumn<DateTime>(
                name: "PoximaFechaPosible",
                table: "Periodos",
                type: "datetime2",
                nullable: true);
        }
    }
}
