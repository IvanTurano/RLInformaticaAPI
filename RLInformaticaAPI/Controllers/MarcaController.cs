using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLInformaticaAPI.DTOS;
using RLInformaticaAPI.Entidades;

namespace RLInformaticaAPI.Controllers
{
    [ApiController]
    [Route("api/marca")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MarcaController : CustomBaseController
    {
        public MarcaController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<MarcaDTO>>> Get()
        {
            return await Get<Marca, MarcaDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerMarca")]
        public async Task<ActionResult<MarcaDTO>> Get(int id)
        {
            return await Get<Marca,MarcaDTO>(id); 
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]MarcaCreacionDTO marcaCreacionDTO)
        {
            return await Post<MarcaCreacionDTO, Marca, MarcaDTO>(marcaCreacionDTO, "obtenerMarca");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] MarcaCreacionDTO marcaCreacionDTO)
        {
            return await Put<MarcaCreacionDTO, Marca>(id, marcaCreacionDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Marca>(id);
        }
    }
}
