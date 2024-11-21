using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using TiendaOnline.Models;
using TiendaOnline.Models.Usuario;

namespace TiendaOnline.Data
{
    public class GestionInventarioContext(DbContextOptions<GestionInventarioContext> options) : DbContext(options)
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la herencia de Usuario con discriminador
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Usuario>("Usuario")
                .HasValue<Cliente>("Cliente")
                .HasValue<Admin>("Admin");

            // Configurar el esquema 'inventario' para cada tabla
            modelBuilder.Entity<Producto>().ToTable("Productos", schema: "inventario");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos", schema: "inventario");
            modelBuilder.Entity<DetallePedido>().ToTable("DetallePedido", schema: "inventario");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios", schema: "inventario");

            // Relación entre Pedido y Cliente (asegurarte de que ClienteID existe en Pedido)
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasColumnType("decimal(18,2)");

            // Relación entre DetallePedido y Pedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.PedidoID);            

            // Relación entre DetallePedido y Producto
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Producto)
                .WithMany()
                .HasForeignKey(dp => dp.ProductoID);

            modelBuilder.Entity<DetallePedido>()
                .Property(p => p.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");
        }
    }

}