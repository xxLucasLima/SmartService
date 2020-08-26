using System;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : Controller
    {
        public IRepository _repo { get; }
        public EmpresaController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> Post(Empresa model)
        {
            try
            {
                model.DataCriacao = DateTime.Today;

                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/empresa-form/{model.Id_Empresa}", model);
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
                var result = await _repo.GetAllEmpresasAsync();

                return Ok(result);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("{idEmpresa}")]
        public async Task<IActionResult> GetEmpresaAsyncById(int idEmpresa)
        {
            try
            {
                var result = await _repo.GetEmpresaAsyncById(idEmpresa);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpDelete("{idEmpresa}")]
        public async Task<IActionResult> Delete(int idEmpresa)
        {
            try
            {
                var empresa = await _repo.GetEmpresaAsyncById(idEmpresa);

                if (empresa == null)
                    return NotFound();

                _repo.Delete(empresa);

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

        [HttpPut("{idEmpresa}")]
        public async Task<IActionResult> Put(int idEmpresa, Empresa payload)
        {
            try
            {
                var empresa = await _repo.GetEmpresaAsyncById(idEmpresa);

                if (empresa == null)
                    return NotFound();

                _repo.Update(payload);

                if (await _repo.SaveChangesAsync())
                {
                    empresa = await _repo.GetEmpresaAsyncById(idEmpresa);
                    return Created($"/api/empresa/{payload.Id_Empresa}", empresa);
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