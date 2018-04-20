using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Termek.Migrations
{
    public partial class ModificadoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Produto",
                newName: "Modelo");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Produto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                table: "Produto",
                newName: "Nome");
        }
    }
}
