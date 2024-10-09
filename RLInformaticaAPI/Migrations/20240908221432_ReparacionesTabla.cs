using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RLInformaticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReparacionesTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reparaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaDeIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispositivoId = table.Column<int>(type: "int", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Falla = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NombreCliente = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ContactoCliente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ManoDeObra = table.Column<int>(type: "int", nullable: false),
                    Repuestos = table.Column<int>(type: "int", nullable: false),
                    PrecioFinal = table.Column<int>(type: "int", nullable: false),
                    Terminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Dispositivos_DispositivoId",
                        column: x => x.DispositivoId,
                        principalTable: "Dispositivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_DispositivoId",
                table: "Reparaciones",
                column: "DispositivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_MarcaId",
                table: "Reparaciones",
                column: "MarcaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reparaciones");
        }
    }
}
