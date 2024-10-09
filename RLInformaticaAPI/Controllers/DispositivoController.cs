using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLInformaticaAPI.DTOS;
using RLInformaticaAPI.Entidades;

namespace RLInformaticaAPI.Controllers
{
    [ApiController]
    [Route("api/dispositivo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DispositivoController : CustomBaseController
    {
        public DispositivoController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        { 
        }

        [HttpGet]
        public async Task<ActionResult<List<DispositivoDTO>>> Get()
        {
            return await Get<Dispositivo, DispositivoDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerDispositivo")]
        public async Task<ActionResult<DispositivoDTO>> Get(int id)
        {
            return await Get<Dispositivo,DispositivoDTO>(id); 
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DispositivoCreacionDTO dispositivoCreacionDTO)
        {
            return await Post<DispositivoCreacionDTO, Dispositivo, DispositivoDTO>(dispositivoCreacionDTO, "obtenerDispositivo");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] DispositivoCreacionDTO dispositivoCreacionDTO)
        {
            return await Put<DispositivoCreacionDTO, Dispositivo>(id, dispositivoCreacionDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Dispositivo>(id);
        }
    }
}
