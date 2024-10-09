using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RLInformaticaAPI.DTOS;
using RLInformaticaAPI.Entidades;
using RLInformaticaAPI.Helpers;
using System.Security.Claims;

namespace RLInformaticaAPI.Controllers
{
    [ApiController]
    [Route("api/reparacion")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReparacionController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReparacionController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReparacionDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Reparaciones
                .AsNoTracking()
                .Include(x => x.Dispositivo)
                .Include(x => x.Marca)
                .Include(x => x.Empleado)
                .AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistrosPorPagina);
            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ReparacionDTO>>(entidades);
        }

        [HttpGet("{terminada:bool}")]
        public async Task<ActionResult<List<ReparacionDTO>>> Get(bool terminada,[FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Reparaciones
                .AsNoTracking()
                .Include(x => x.Dispositivo)
                .Include(x => x.Marca)
                .Include(x => x.Empleado)
                .AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistrosPorPagina);
            var entidades = await queryable.Paginar(paginacionDTO).Where(x => x.Terminada == terminada).ToListAsync();
            return mapper.Map<List<ReparacionDTO>>(entidades);
        }

        [HttpGet("MisReparaciones")]
        public async Task<ActionResult<List<ReparacionDTO>>> MisReparaciones([FromQuery] PaginacionDTO paginacionDTO)
        {
            var empleadoId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var queryable = context.Reparaciones
                .AsNoTracking()
                .Include(x => x.Dispositivo)
                .Include(x => x.Marca)
                .Include(x => x.Empleado)
                .AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistrosPorPagina);
            var entidades = await queryable.Paginar(paginacionDTO).Where(x => x.EmpleadoId == empleadoId).ToListAsync();
            return mapper.Map<List<ReparacionDTO>>(entidades);
        }

        [HttpGet("{id}", Name = "obtenerReparacion")]
        public async Task<ActionResult<ReparacionDTO>> Get(int id)
        {
            var entidad = await context.Reparaciones
                .Include(x => x.Dispositivo)
                .Include(x => x.Marca)
                .Include(x => x.Empleado)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return mapper.Map<ReparacionDTO>(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReparacionCreacionDTO reparacionCreacionDTO)
        {
            return await Post<ReparacionCreacionDTO, Reparacion, ReparacionDTO>(reparacionCreacionDTO, "obtenerReparacion");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ReparacionCreacionDTO reparacionCreacionDTO)
        {
            return await Put<ReparacionCreacionDTO, Reparacion>(id, reparacionCreacionDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ReparacionPatchDTO> patchDocument)
        {
            if(patchDocument == null) { return BadRequest(); }

            var entidadDB = await context.Reparaciones.FirstOrDefaultAsync(x => x.Id == id);

            if(entidadDB == null) { return NotFound(); }

            var entidadDTO = mapper.Map<ReparacionPatchDTO>(entidadDB);

            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);

            if (!esValido) { return BadRequest(); }

            mapper.Map(entidadDTO, entidadDB);

            await context.SaveChangesAsync();

            return NoContent();
        }  

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Reparacion>(id);
        }
    }
}
