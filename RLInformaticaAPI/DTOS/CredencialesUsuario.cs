using System.ComponentModel.DataAnnotations;

namespace RLInformaticaAPI.DTOS
{
    public class CredencialesUsuario
    {
        [Required]
        [StringLength(40)]
        public string NombreDeUsuario { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
