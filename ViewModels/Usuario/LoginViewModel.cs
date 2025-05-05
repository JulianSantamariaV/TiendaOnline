using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.ViewModels.Usuario
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo electrónico válida.")]
        public  string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }
        [Display(Name = "Recordarme")]
        public bool Recordarme { get; set; }
    }
}
