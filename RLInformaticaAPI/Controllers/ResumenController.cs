using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RLInformaticaAPI.DTOS;
using System.Security.Claims;

namespace RLInformaticaAPI.Controllers
{
    [ApiController]
    [Route("api/resumen")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ResumenController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ResumenController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("gananciaGeneral")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<ResumenGeneralDTO>> GananciaGeneral(int año, int mes)
        {
            var entidades = await context.Reparaciones.Where(x => x.FechaDeIngreso.Year == año && x.FechaDeIngreso.Month == mes).ToListAsync();
            var cantReps = entidades.Count;
            var repsEntregadas = entidades.Count(x => x.Entregada == true);
            var repsTerminadas = entidades.Count(x => x.Terminada == true);
            var ganancia = 0;
            var gastosEnRepuestos = 0;

            foreach (var entidad in entidades)
            {
                ganancia += entidad.ManoDeObra;
                gastosEnRepuestos += entidad.Repuestos;
            }

            var resumenMensualGeneral = new ResumenGeneralDTO
            {
                cantReparaciones = cantReps,
                gastosEnRepuestos = gastosEnRepuestos,
                gananciaMensual = ganancia,
                reparacionesTerminadasSinEntregar = repsTerminadas,
                reparacionesTerminadasYEntregadas = repsEntregadas
            };

            return resumenMensualGeneral;
        }

        [HttpGet("gananciaIndividual")]
        public async Task<ActionResult<ResumenGeneralDTO>> GananciaIndividual(int año, int mes)
        {
            var empleadoId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var entidades = await context.Reparaciones.Where(x => x.FechaDeIngreso.Year == año && 
            x.FechaDeIngreso.Month == mes && 
            x.EmpleadoId == empleadoId)
                .ToListAsync();
            var cantReps = entidades.Count;
            var repsEntregadas = entidades.Count(x => x.Entregada == true);
            var repsTerminadas = entidades.Count(x => x.Terminada == true);
            var ganancia = 0;
            var gastosEnRepuestos = 0;

            foreach (var entidad in entidades)
            {
                ganancia += entidad.ManoDeObra;
                gastosEnRepuestos += entidad.Repuestos;
            }

            var resumenMensualGeneral = new ResumenGeneralDTO
            {
                cantReparaciones = cantReps,
                gastosEnRepuestos = gastosEnRepuestos,
                gananciaMensual = ganancia,
                reparacionesTerminadasSinEntregar = repsTerminadas,
                reparacionesTerminadasYEntregadas = repsEntregadas
            };

            return resumenMensualGeneral;
        }

    }
}
