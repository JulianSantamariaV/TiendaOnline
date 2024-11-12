using TiendaOnline.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura el contexto de base de datos
builder.Services.AddDbContext<GestionInventarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaOnlineDB")));

// Habilitar el uso de sesiones
builder.Services.AddSession();

// Otros servicios
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activar el uso de sesiones
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
