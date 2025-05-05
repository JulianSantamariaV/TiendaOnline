using TiendaOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using TiendaOnline.Repositories;
using TiendaOnline.Models.Usuario;
using TiendaOnline.Factories;
using TiendaOnline.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura el contexto de base de datos
builder.Services.AddDbContext<GestionInventarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaOnlineDB")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductoFactory, ProductoFactory>();
builder.Services.AddScoped<ShoppingCartService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
    .AddPolicy("Cliente", policy => policy.RequireRole("Cliente"));

// Habilitar el uso de sesiones
builder.Services.AddSession( options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de espera de la sesión
    options.Cookie.HttpOnly = true; // La cookie de sesión no es accesible desde JavaScript
    options.Cookie.IsEssential = true; // La cookie es esencial para el funcionamiento de la aplicación
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
