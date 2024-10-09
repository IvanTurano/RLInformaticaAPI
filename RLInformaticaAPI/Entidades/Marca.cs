using System.ComponentModel.DataAnnotations;

namespace RLInformaticaAPI.Entidades
{
    public class Marca : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
