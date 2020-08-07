using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using back_end.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public IRepository _repo { get; }
        public AuthenticationController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(Usuario usuario)
        {
            var model = await _repo.GetUsuarioAsyncByEmailAndPassword(usuario.Email, usuario.Senha);

            if (model == null)
                return new NotFoundObjectResult(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(model);
            model.Senha = "";
            var p = new
            {
                usuario = model,
                token = token
            };
            return p;

        }

        [HttpGet]
        public ActionResult<dynamic> LoadSession()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity.IsAuthenticated)
            {
                Usuario user = new Usuario();
                user.PerfilUsuario = new PerfilUsuario();
                IEnumerable<Claim> claims = identity.Claims;
                var email = claims.Where(p => p.Type.Contains("name")).FirstOrDefault()?.Value;
                var token = Request.Headers.GetCommaSeparatedValues("Authorization")[0].Split(" ")[1];
                var nome = claims.Where(p => p.Type.Contains("nameidentifier")).FirstOrDefault()?.Value;
                var role = claims.Where(p => p.Type.Contains("role")).FirstOrDefault()?.Value;

                user.Nome = nome;
                user.PerfilUsuario.Nome = role;
                user.Email = email;
                return new
                {
                    usuario = user,
                    token = token
                };

            }
            else
            {
                return new NotFoundObjectResult(new { message = "Sua sessão é inválida ou está expirada" });

            }
        }
    }
}