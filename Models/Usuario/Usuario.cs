using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.Usuario
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
}
