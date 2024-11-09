using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;
using TiendaOnline.Models;

namespace TiendaOnline.Data
{
    public class GestionInventarioContext : DbContext
    {
        public GestionInventarioContext(DbContextOptions<GestionInventarioContext> options) : base(options) { }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar el esquema 'inventario' para cada tabla
            modelBuilder.Entity<Producto>().ToTable("Productos", schema: "inventario");
            modelBuilder.Entity<Cliente>().ToTable("Clientes", schema: "inventario");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos", schema: "inventario");
            modelBuilder.Entity<DetallePedido>().ToTable("DetallePedido", schema: "inventario");


            // Configuración adicional de las relaciones

           

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteID);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.PedidoID);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Producto)
                .WithMany()
                .HasForeignKey(dp => dp.ProductoID);
        }
    }
}