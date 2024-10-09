using RLInformaticaAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace RLInformaticaAPI.DTOS
{
    public class ReparacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaDeIngreso { get; set; }
        public string Dispositivo { get; set; }
        public string Marca { get; set; }
        public string? Modelo { get; set; }
        public string Falla { get; set; }
        public string? Detalles { get; set; }
        public string? NombreCliente { get; set; }
        public string? ContactoCliente { get; set; }
        public int ManoDeObra { get; set; }
        public int Repuestos { get; set; }
        public int PrecioFinal { get; set; }
        public string Empleado { get; set; }
        public bool Terminada { get; set; }
        public bool Entregada { get; set; } 

    }
}
