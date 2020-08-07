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
                    return Created($"/usuario-form/{model.Id_Usuario}", model);
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
                var result = await _repo.GetAllUsuariosAsync();

                return Ok(result);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpDelete("{Id_Usuario}")]
        public async Task<IActionResult> Delete(int Id_Usuario)
        {
            try
            {
                var usuario = await _repo.GetUsuarioAsyncById(Id_Usuario);

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

        [HttpGet("{Id_Usuario}")]
        public async Task<IActionResult> GetUsuarioAsyncById(int alunoId)
        {
            try
            {
                var result = await _repo.GetUsuarioAsyncById(alunoId);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("getAllEmpresasDDL/")]
        public async Task<IActionResult> GetAllEmpresasDDL()
        {
            try
            {
                Empresa[] returns = await _repo.GetAllEmpresasDDL();
                List<Empresa> empresas = new List<Empresa>(returns);

                List<Empresa> results = new List<Empresa>();

                if (empresas != null & empresas.Count > 0)
                {
                    foreach (Empresa item in empresas)
                    {
                        Empresa e = new Empresa();
                        e.razaoSocial = item.razaoSocial;
                        e.Id_Empresa = item.Id_Empresa;
                        results.Add(e);
                    }
                }

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("getAllPerfisUsuarioDDL/")]
        public async Task<IActionResult> GetAllPerfisUsuarioDDL()
        {
            try
            {
                PerfilUsuario[] returns = await _repo.GetAllPerfisUsuarioDDL();
                List<PerfilUsuario> perfisUsuario = new List<PerfilUsuario>(returns);

                List<PerfilUsuario> results = new List<PerfilUsuario>();

                if (perfisUsuario != null & perfisUsuario.Count > 0)
                {
                    foreach (PerfilUsuario item in perfisUsuario)
                    {
                        PerfilUsuario e = new PerfilUsuario();
                        e.Nome = item.Nome;
                        e.Id_PerfilUsuario = item.Id_PerfilUsuario;
                        results.Add(e);
                    }
                }
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

    }
}