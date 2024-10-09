using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RLInformaticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Empleadoyreparacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Repuestos",
                table: "Reparaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrecioFinal",
                table: "Reparaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ManoDeObra",
                table: "Reparaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "EmpleadoId",
                table: "Reparaciones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Entregada",
                table: "Reparaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_EmpleadoId",
                table: "Reparaciones",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reparaciones_AspNetUsers_EmpleadoId",
                table: "Reparaciones",
                column: "EmpleadoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reparaciones_AspNetUsers_EmpleadoId",
                table: "Reparaciones");

            migrationBuilder.DropIndex(
                name: "IX_Reparaciones_EmpleadoId",
                table: "Reparaciones");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Reparaciones");

            migrationBuilder.DropColumn(
                name: "Entregada",
                table: "Reparaciones");

            migrationBuilder.AlterColumn<int>(
                name: "Repuestos",
                table: "Reparaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrecioFinal",
                table: "Reparaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManoDeObra",
                table: "Reparaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
