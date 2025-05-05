using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaOnline.Migrations
{
    /// <inheritdoc />
    public partial class CambiarTipoUsuarioAInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TipoUsuario",
                schema: "inventario",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldDefaultValue: "1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoUsuario",
                schema: "inventario",
                table: "Usuarios",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "1",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }
    }
}
