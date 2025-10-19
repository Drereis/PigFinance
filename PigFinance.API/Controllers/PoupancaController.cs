using Microsoft.AspNetCore.Mvc;
using PigFinance.API.Models;
using PigFinance.API.Services;
using System.Collections.Generic;
using System.Linq;

namespace PigFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoupancasController : ControllerBase
    {
        private readonly IPoupancaService _service;

        private readonly int _usuarioId = 1;
        public PoupancasController(IPoupancaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Poupanca>> Get()
        {
            try
            {
                var poupancas = _service.GetAll(_usuarioId);

                if (!poupancas.Any())
                {
                    return NotFound("Nenhuma meta de poupança encontrada.");
                }

                return Ok(poupancas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar poupanças: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao listar as metas de poupança.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Poupanca> Get(int id)
        {
            try
            {
                var poupanca = _service.GetById(id, _usuarioId);

                if (poupanca == null)
                {
                    return NotFound($"Meta de poupança com ID {id} não encontrada.");
                }

                return Ok(poupanca);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar poupança: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao buscar a meta de poupança.");
            }
        }

        [HttpPost]
        public ActionResult<Poupanca> Post([FromBody] Poupanca poupanca)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                poupanca.UsuarioId = _usuarioId;

                var novaPoupanca = _service.Add(poupanca);

                return CreatedAtAction(nameof(Get), new { id = novaPoupanca.Id }, novaPoupanca);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar poupança: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao criar a meta de poupança.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Poupanca poupanca)
        {
            try
            {
                if (id != poupanca.Id)
                {
                    return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                poupanca.UsuarioId = _usuarioId;

                var poupancaAtualizada = _service.Update(poupanca);

                if (poupancaAtualizada == null)
                {
                    return NotFound($"Meta de poupança com ID {id} não encontrada ou não pertence a você.");
                }

                return NoContent(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar poupança: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao atualizar a meta de poupança.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var poupancaExistente = _service.GetById(id, _usuarioId);

                if (poupancaExistente == null)
                {
                    return NotFound($"Meta de poupança com ID {id} não encontrada ou não pertence a você.");
                }

                _service.Delete(id, _usuarioId);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar poupança: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao deletar a meta de poupança.");
            }
        }
    }
}
