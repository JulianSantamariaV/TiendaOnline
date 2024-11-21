using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaOnline.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndPedidoRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inventario");

            migrationBuilder.CreateTable(
                name: "Productos",
                schema: "inventario",
                columns: table => new
                {
                    ProductoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadInventario = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrlSecond = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "inventario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Admin_Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                schema: "inventario",
                columns: table => new
                {
                    PedidoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoID);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_ClienteID",
                        column: x => x.ClienteID,
                        principalSchema: "inventario",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedido",
                schema: "inventario",
                columns: table => new
                {
                    DetalleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedido", x => x.DetalleID);
                    table.ForeignKey(
                        name: "FK_DetallePedido_Pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalSchema: "inventario",
                        principalTable: "Pedidos",
                        principalColumn: "PedidoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePedido_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalSchema: "inventario",
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_PedidoID",
                schema: "inventario",
                table: "DetallePedido",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_ProductoID",
                schema: "inventario",
                table: "DetallePedido",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteID",
                schema: "inventario",
                table: "Pedidos",
                column: "ClienteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePedido",
                schema: "inventario");

            migrationBuilder.DropTable(
                name: "Pedidos",
                schema: "inventario");

            migrationBuilder.DropTable(
                name: "Productos",
                schema: "inventario");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "inventario");
        }
    }
}
