using System;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoteController : Controller
    {
        public IRepository _repo { get; }

        public LoteController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> Post(Lote model)
        {
            try
            {
                model.DataCriacao = DateTime.Today;
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/lote-form/{model.Id_Lote}", model);
                }
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou" + e.Message);
            }

            return BadRequest();

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllLotesAsync();

                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou " + ex.Message);
            }
        }

        [HttpGet("{idLote}")]
        public async Task<IActionResult> GetLoteAsyncById(int idLote)
        {
            try
            {
                var result = await _repo.GetLoteAsyncById(idLote);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }


        [HttpDelete("{idLote}")]
        public async Task<IActionResult> Delete(int idLote)
        {
            try
            {
                var usuario = await _repo.GetLoteAsyncById(idLote);

                if (usuario == null)
                    return NotFound();

                _repo.Delete(usuario);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest();

        }

        [HttpPut("{idLote}")]
        public async Task<IActionResult> Put(int idLote, Lote payload)
        {
            try
            {
                var usuario = await _repo.GetLoteAsyncById(idLote);

                if (usuario == null)
                    return NotFound();

                _repo.Update(payload);

                if (await _repo.SaveChangesAsync())
                {
                    usuario = await _repo.GetLoteAsyncById(idLote);
                    return Created($"/api/usuario/{payload.Id_Lote}", usuario);
                }
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + ex.Message);
            }
            return BadRequest();

        }

    }
}