using System;
using System.Data;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : Controller
    {
        public IRepository _repo { get; }

        public ClienteController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente model)
        {
            try
            {
                model.DataCriacao = DateTime.Today;
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/lote-form/{model.Id_Cliente}", model);
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
                var result = await _repo.GetAllClientesAsync();

                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou " + ex.Message);
            }
        }

        [HttpGet("{idCliente}")]
        public async Task<IActionResult> GetClienteAsyncById(int idCliente)
        {
            try
            {
                var result = await _repo.GetClienteAsyncById(idCliente);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }


        [HttpDelete("{idCliente}")]
        public async Task<IActionResult> Delete(int idCliente)
        {
            try
            {
                var usuario = await _repo.GetClienteAsyncById(idCliente);

                if (usuario == null)
                    return NotFound();

                _repo.Delete(usuario);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (DbUpdateException )
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, "Não é possível excluir Cliente. Favor verificar se há produtos atrelados a este cliente ");
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + ex.Message);
            }
            return BadRequest();

        }

        [HttpPut("{idCliente}")]
        public async Task<IActionResult> Put(int idCliente, Cliente payload)
        {
            try
            {
                var usuario = await _repo.GetClienteAsyncById(idCliente);

                if (usuario == null)
                    return NotFound();

                _repo.Update(payload);

                if (await _repo.SaveChangesAsync())
                {
                    usuario = await _repo.GetClienteAsyncById(idCliente);
                    return Created($"/api/usuario/{payload.Id_Cliente}", usuario);
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