using System;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        public IRepository _repo { get; }
        public UsuarioController(IRepository repo)
        {
            _repo = repo;

        }
        [HttpPost]
        public async Task<IActionResult> Post(Usuario model)
        {
            try
            {
                model.DataCriacao = DateTime.Today;

                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/aluno/{model.Id_Usuario}", model);
                }

            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou" + e.Message);
            }

            return BadRequest();

        }
    }
}