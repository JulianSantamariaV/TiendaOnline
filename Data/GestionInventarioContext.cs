using Microsoft.EntityFrameworkCore;
using TiendaOnline.Models;
using TiendaOnline.Models.ShoppingCart;
using TiendaOnline.Models.Usuario;

namespace TiendaOnline.Data
{
    public class GestionInventarioContext(DbContextOptions<GestionInventarioContext> options) : DbContext(options)
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la herencia de Usuario con discriminador
            modelBuilder.Entity<Usuario>()
             .HasDiscriminator<int>("TipoUsuario")
             .HasValue<Usuario>(0)
             .HasValue<Cliente>(1)
             .HasValue<Admin>(2);

            // Configurar el esquema 'inventario' para cada tabla
            modelBuilder.Entity<Producto>().ToTable("Productos", schema: "inventario");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos", schema: "inventario");
            modelBuilder.Entity<DetallePedido>().ToTable("DetallePedido", schema: "inventario");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios", schema: "inventario");
            modelBuilder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItems", schema: "inventario");

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

            modelBuilder.Entity<Usuario>(u =>
            {
                u.HasKey(u => u.UsuarioId);
                u.Property(u => u.UsuarioId)
                .IsRequired();

                u.Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

                u.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
                u.HasIndex(u => u.Email)
                .IsUnique();

                u.Property(u => u.PasswordHash)
                .HasMaxLength(100)
                .IsRequired();

                u.Property(u => u.TipoUsuario)
                .HasDefaultValue(1);

            });

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.ShoppingCartItems)
                .WithOne(i => i.Usuario)
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(c => c.Producto)
                .WithMany()
                .HasForeignKey(c => c.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(p => p.Cantidad)
                .IsRequired();

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}