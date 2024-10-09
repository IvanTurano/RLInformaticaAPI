namespace RLInformaticaAPI.DTOS
{
    public class ResumenGeneralDTO
    {
        public int cantReparaciones {  get; set; }
        public int gananciaMensual { get; set; }
        public int gastosEnRepuestos { get; set; }
        public int reparacionesTerminadasYEntregadas { get; set; }
        public int reparacionesTerminadasSinEntregar { get; set; }

    }
}
