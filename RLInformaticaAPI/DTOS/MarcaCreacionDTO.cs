using System.ComponentModel.DataAnnotations;

namespace RLInformaticaAPI.DTOS
{
    public class MarcaCreacionDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
