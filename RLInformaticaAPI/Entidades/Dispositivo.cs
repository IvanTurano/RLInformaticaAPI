using System.ComponentModel.DataAnnotations;

namespace RLInformaticaAPI.Entidades
{
    public class Dispositivo : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }

    }
}
