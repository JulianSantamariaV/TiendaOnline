using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TiendaOnline.Models.Usuario;
using TiendaOnline.Repositories;


namespace TiendaOnline.Controllers
{
    public class AuthController(IRepository<Usuario> usuarioRepository) : Controller
    {
        private readonly IRepository<Usuario> _usuarioRepository = usuarioRepository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe un usuario con el mismo email
                var existingUser = await _usuarioRepository.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "El correo ya está registrado.");
                    return View(model);
                }

                // Encriptar contraseña
                model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);

                // Guardar usuario en la base de datos
                await _usuarioRepository.AddAsync(model);

                // Redirigir al login
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Buscar el usuario por email
            var user = await _usuarioRepository.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                // Crear las claims del usuario
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Nombre),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Role, user is Admin ? "Admin" : "Cliente")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Iniciar sesión
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirigir según el rol del usuario
                return user is Admin
                    ? RedirectToAction("Index", "Admin")
                    : RedirectToAction("Index", "Home");
            }

            // Mostrar mensaje de error si el login falla
            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Cerrar sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
