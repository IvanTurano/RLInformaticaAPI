using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RLInformaticaAPI.DTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RLInformaticaAPI.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registrar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuario.NombreDeUsuario, Email = credencialesUsuario.NombreDeUsuario };
            var resultado = await userManager.CreateAsync(usuario,credencialesUsuario.Password);

            if (resultado.Succeeded)
            {
                return await ConstuirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.NombreDeUsuario, credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstuirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("login incorrecto");
            }
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
        {
            var nombreClaim = HttpContext.User.Claims.Where(claim => claim.Type == "nombre").FirstOrDefault();
            var nombre = nombreClaim.Value;
            var credencialesUsuario = new CredencialesUsuario()
            {
                NombreDeUsuario = nombre
            };

            return await ConstuirToken(credencialesUsuario);
        }

        [HttpGet("validarToken")]
        public ActionResult ValidarToken([FromQuery] string token)
        {
            bool respuesta = validarToken(token);
            return StatusCode(StatusCodes.Status200OK, new ValidarTokenDTO { EsValido = respuesta });
        }

        private async Task<RespuestaAutenticacion> ConstuirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("nombre",credencialesUsuario.NombreDeUsuario),
               
            };

            var usuario = await userManager.FindByNameAsync(credencialesUsuario.NombreDeUsuario);

            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id));

            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };

        }

        private bool validarToken(string token)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var tokenHandle = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"])),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                claimsPrincipal = tokenHandle.ValidateToken(token, validationParameters, out SecurityToken validationToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost("HacerAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> HacerAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByNameAsync(editarAdminDTO.nombreDeUsuario);
            await userManager.AddClaimAsync(usuario, new Claim("Admin","1"));
            return NoContent();
        }

        [HttpPost("RemoverAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByNameAsync(editarAdminDTO.nombreDeUsuario);
            await userManager.RemoveClaimAsync(usuario, new Claim("Admin", "1"));
            return NoContent();
        }

        [HttpGet("TraerUsuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<List<UsuarioDTO>>> TraerUsuarios()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var users = await userManager.Users
                .Where(x => x.Id != userId)
                .Select(user => new UsuarioDTO
                {
                    Id = user.Id,
                    NombreDeUsuario = user.UserName,
                   
                })
                .ToListAsync();

            return users;
           
        }


        [HttpPut("{id}",Name = "editarUsuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> EditarUsuario(string id, [FromBody] EditarUsuarioDTO modelo)
        {
            var usuario = await userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            
            if (!string.IsNullOrWhiteSpace(modelo.NombreDeUsuario))
            {
                var resultUserName = await userManager.SetUserNameAsync(usuario, modelo.NombreDeUsuario);
                if (!resultUserName.Succeeded)
                {
                    return BadRequest(resultUserName.Errors);
                }
            }

            if (!string.IsNullOrWhiteSpace(modelo.CorreoElectronico))
            {
                var resultEmail = await userManager.SetEmailAsync(usuario, modelo.CorreoElectronico);
                if (!resultEmail.Succeeded)
                {
                    return BadRequest(resultEmail.Errors);
                }
            }
            
            if (!string.IsNullOrWhiteSpace(modelo.NuevaContrasena))
            {
                
                var removePasswordResult = await userManager.RemovePasswordAsync(usuario);
                if (!removePasswordResult.Succeeded)
                {
                    return BadRequest(removePasswordResult.Errors);
                }

                var addPasswordResult = await userManager.AddPasswordAsync(usuario, modelo.NuevaContrasena);
                if (!addPasswordResult.Succeeded)
                {
                    return BadRequest(addPasswordResult.Errors);
                }
            }

            var updateResult = await userManager.UpdateAsync(usuario);
            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult.Errors);
            }

            return Ok("Usuario actualizado correctamente");
        }

    }
}
