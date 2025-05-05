using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TiendaOnline.Data;
using TiendaOnline.Models.ShoppingCart;
using TiendaOnline.Models.Usuario;
using TiendaOnline.Repositories;
using TiendaOnline.Services;
using TiendaOnline.ViewModels.Usuario;

namespace TiendaOnline.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly GestionInventarioContext _context;
        private readonly ShoppingCartService _shoppingCartService;

        // Constructor para inyectar dependencias
        public AuthController(IRepository<Usuario> usuarioRepository, ILogger<AuthController> logger,
                              GestionInventarioContext context, ShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _context = context;
            _shoppingCartService = shoppingCartService;  // Ahora inyectamos ShoppingCartService
        }

        public IActionResult Login()
        {
            _logger.LogInformation("Se accedió a la vista de login.");
            var model = new LoginViewModel();
            return View(model);
        }

        public IActionResult Register()
        {
            _logger.LogInformation("Se accedió a la vista de registro.");
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                _logger.LogWarning("Registro fallido: las contraseñas no coinciden.");
                ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden.");
                return View("Register", model);
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning("Error de validación: {Error}", error.ErrorMessage);
                }
                _logger.LogWarning("Registro fallido: datos inválidos.");
                return View("Register", model);
            }

            var existingUser = await _usuarioRepository.FirstOrDefaultAsync(u => u.Email == model.Email);

            _logger.LogInformation("Creando usuario antes de persistir en base de datos...");

            if (existingUser != null)
            {
                _logger.LogWarning("Intento de registro con correo ya registrado: {Email}", model.Email);
                ModelState.AddModelError("Email", "El correo ya está registrado.");
                return View("Register", model);
            }

            var nuevoUsuario = new Cliente
            {
                Nombre = model.Nombre,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            };

            await _usuarioRepository.AddAsync(nuevoUsuario);
            _logger.LogInformation("Nuevo usuario registrado: {Email}", model.Email);

            TempData["Success"] = "Registro exitoso. Ya puedes iniciar sesión.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login fallido: datos inválidos.");
                return View("Login", model);
            }

            var user = await _usuarioRepository.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Nombre),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Role, user is Admin ? "Admin" : "Cliente"),
                    new(ClaimTypes.NameIdentifier, user.UsuarioId.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                await MigrateSessionCartToDatabase(user.UsuarioId);

                _logger.LogInformation("Login exitoso: {Email}", user.Email);

                return user is Admin
                    ? RedirectToAction("Index", "Home")
                    : RedirectToAction("Index", "Home");
            }

            _logger.LogWarning("Login fallido: credenciales inválidas para {Email}", model.Email);
            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View("Login", model);
        }

        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Cierre de sesión iniciado.");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task MigrateSessionCartToDatabase(int usuarioId)
        {
            var sessionItems = _shoppingCartService.GetCartItemsFromSession(HttpContext.Session); // Usamos la instancia inyectada
            foreach (var item in sessionItems)
            {
                var existing = await _context.ShoppingCartItems
                    .FirstOrDefaultAsync(i => i.UsuarioId == usuarioId && i.ProductoID == item.ProductoID);

                if (existing != null)
                {
                    existing.Cantidad += item.Cantidad;
                }
                else
                {
                    _context.ShoppingCartItems.Add(new ShoppingCartItem
                    {
                        UsuarioId = usuarioId,
                        ProductoID = item.ProductoID,
                        Cantidad = item.Cantidad
                    });
                }
            }

            await _context.SaveChangesAsync();
            _shoppingCartService.ClearCart(HttpContext.Session);  // Usamos la instancia inyectada
        }
    }
}
