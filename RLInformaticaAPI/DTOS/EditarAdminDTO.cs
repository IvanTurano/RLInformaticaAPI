using System.ComponentModel.DataAnnotations;

namespace RLInformaticaAPI.DTOS
{
    public class EditarAdminDTO
    {
        [Required]
        public string nombreDeUsuario {  get; set; }

    }
}
