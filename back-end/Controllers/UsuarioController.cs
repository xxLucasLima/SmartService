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

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> GetUsuarioAsyncById(int idUsuario)
        {
            try
            {
                var result = await _repo.GetUsuarioAsyncById(idUsuario);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("resetPassUsuario/{idUsuario}")]
        public async Task<IActionResult> ResetPassUsuario(int idUsuario)
        {
            try
            {
                var usuario = await _repo.GetUsuarioAsyncById(Convert.ToInt32(idUsuario));

                if (usuario == null)
                    return NotFound();

                usuario.Senha = null;

                _repo.Update(usuario);

                if (await _repo.SaveChangesAsync())
                {
                    usuario = await _repo.GetUsuarioAsyncById(Convert.ToInt32(idUsuario));
                    return Created($"/api/usuario/{usuario.Id_Usuario}", usuario);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + ex.Message);
            }

            return BadRequest();

        }


        [HttpDelete("{idUsuario}")]
        public async Task<IActionResult> Delete(int idUsuario)
        {
            try
            {
                var usuario = await _repo.GetUsuarioAsyncById(idUsuario);

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

        [HttpPut("{idUsuario}")]
        public async Task<IActionResult> Put(int idUsuario, Usuario payload)
        {
            try
            {
                var usuario = await _repo.GetUsuarioAsyncById(idUsuario);

                if (usuario == null)
                    return NotFound();

                _repo.Update(payload);

                if (await _repo.SaveChangesAsync())
                {
                    usuario = await _repo.GetUsuarioAsyncById(idUsuario);
                    return Created($"/api/usuario/{payload.Id_Usuario}", usuario);
                }
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + ex.Message);
            }
            return BadRequest();

        }

        [HttpGet("getAllEmpresasDDL/")]
        public async Task<IActionResult> GetAllEmpresasDDL()
        {
            try
            {
                Empresa[] returns = await _repo.GetAllEmpresasDDL();
                List<Empresa> empresas = new List<Empresa>(returns);

                List<EmpresaDDL> results = new List<EmpresaDDL>();

                if (empresas != null & empresas.Count > 0)
                {
                    foreach (Empresa item in empresas)
                    {
                        EmpresaDDL e = new EmpresaDDL();
                        e.Text = item.RazaoSocial;
                        e.Value = item.Id_Empresa;
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

                List<PerfilUsuarioDDL> results = new List<PerfilUsuarioDDL>();

                if (perfisUsuario != null & perfisUsuario.Count > 0)
                {
                    foreach (PerfilUsuario item in perfisUsuario)
                    {
                        PerfilUsuarioDDL e = new PerfilUsuarioDDL();
                        e.Text = item.Nome;
                        e.Value = item.Id_PerfilUsuario;
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