using System;
using System.Collections.Generic;
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
    public class ProdutoController : Controller
    {
        public IRepository _repo { get; }

        public ProdutoController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> Post(Produto model)
        {
            try
            {
                model.DataCriacao = DateTime.Today;

                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/produto-form/{model.Id_Produto}", model);
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
                var result = await _repo.GetAllProdutosAsync();

                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou "+ ex.Message);
            }
        }

        [HttpGet("{idProduto}")]
        public async Task<IActionResult> GetProdutoAsyncById(int idProduto)
        {
            try
            {
                var result = await _repo.GetProdutoAsyncById(idProduto);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }


        [HttpDelete("{idProduto}")]
        public async Task<IActionResult> Delete(int idProduto)
        {
            try
            {
                var usuario = await _repo.GetProdutoAsyncById(idProduto);

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

        [HttpPut("{idProduto}")]
        public async Task<IActionResult> Put(int idProduto, Produto payload)
        {
            try
            {
                var usuario = await _repo.GetProdutoAsyncById(idProduto);

                if (usuario == null)
                    return NotFound();

                _repo.Update(payload);

                if (await _repo.SaveChangesAsync())
                {
                    usuario = await _repo.GetProdutoAsyncById(idProduto);
                    return Created($"/api/usuario/{payload.Id_Produto}", usuario);
                }
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + ex.Message);
            }
            return BadRequest();

        }

        [HttpGet("getAllClientesDDL/")]
        public async Task<IActionResult> GetAllClientesDDL()
        {
            try
            {
                Cliente[] returns = await _repo.GetAllClientesDDL();
                List<Cliente> clientes = new List<Cliente>(returns);
                List<ClienteDDL> results = new List<ClienteDDL>();

                if (clientes != null & clientes.Count > 0)
                {
                    foreach (Cliente item in clientes)
                    {
                        ClienteDDL e = new ClienteDDL();
                        e.Text = item.NomeFantasia + " - " + item.CnpjCpf;
                        e.Value = item.Id_Cliente;
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

        [HttpGet("getAllLotesDDL/")]
        public async Task<IActionResult> GetAllLotesDDL()
        {
            try
            {
                Lote[] returns = await _repo.GetAllLotesDDL();
                List<Lote> lotes = new List<Lote>(returns);
                List<LoteDDL> results = new List<LoteDDL>();

                if (lotes != null & lotes.Count > 0)
                {
                    foreach (Lote item in lotes)
                    {
                        LoteDDL e = new LoteDDL();
                        e.Text = item.CodEntrada + " - " + item.CodSaida;
                        e.Value = item.Id_Lote;
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

        [HttpGet("getAllDefeitosProdutoDDL/")]
        public async Task<IActionResult> GetAllDefeitosProdutoDDL()
        {
            try
            {
                DefeitoProduto[] returns = await _repo.GetAllDefeitosProdutoDDL();
                List<DefeitoProduto> defeitoProdutos = new List<DefeitoProduto>(returns);
                List<DefeitoProdutoDDL> results = new List<DefeitoProdutoDDL>();

                if (defeitoProdutos != null & defeitoProdutos.Count > 0)
                {
                    foreach (DefeitoProduto item in defeitoProdutos)
                    {
                        DefeitoProdutoDDL e = new DefeitoProdutoDDL();
                        e.Text = item.Descricao;
                        e.Value = item.Id_DefeitoProduto;
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