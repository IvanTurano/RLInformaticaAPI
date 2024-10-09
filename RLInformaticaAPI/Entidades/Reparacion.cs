using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RLInformaticaAPI.Entidades
{
    public class Reparacion : IId
    {
        public int Id { get; set; }
        public DateTime FechaDeIngreso { get; set; }
        public int DispositivoId { get; set; }
        public Dispositivo Dispositivo { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        [StringLength(40)]
        public string? Modelo { get; set; }
        [StringLength(300)]
        public string Falla { get; set; }
        [StringLength(1000)]
        public string? Detalles { get; set; }
        [StringLength(40)]
        public string? NombreCliente { get; set; }
        [StringLength(100)]
        public string? ContactoCliente { get; set; }
        public int ManoDeObra { get; set; }
        public int Repuestos {  get; set; }
        public int PrecioFinal {  get; set; }
        public string EmpleadoId { get; set; }
        public IdentityUser Empleado { get; set; }
        public bool Terminada { get; set; } = false;
        public bool Entregada { get; set; } = false;  

    }
}
