using Microsoft.AspNetCore.Mvc;
using PigFinance.API.Models;
using PigFinance.API.Services; 
using System.Collections.Generic;
using System.Linq;

namespace PigFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase 
    {
        private readonly ITransactionService _service; 

        private readonly int _usuarioId = 1;

        public TransacoesController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get() 
        {
            try
            {
                var transacoes = _service.GetAll(_usuarioId);

                if (transacoes == null || !transacoes.Any())
                {
                    return NotFound("Nenhuma transação encontrada.");
                }

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar transações: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao listar as transações.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Transaction> Get(int id) 
        {
            try
            {
                var transacao = _service.GetById(id, _usuarioId);

                if (transacao == null)
                {
                    return NotFound($"Transação com ID {id} não encontrada.");
                }

                return Ok(transacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar transação: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao buscar a transação.");
            }
        }

        [HttpGet("balance")]
        public ActionResult<decimal> GetTotalBalance()
        {
            try
            {
                decimal balance = _service.GetTotalBalance(_usuarioId);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao calcular saldo total: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao calcular o saldo.");
            }
        }

        [HttpGet("balance-by-period")]
        public ActionResult<decimal> GetBalanceByPeriod(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("A data de início não pode ser maior que a data de fim.");
                }

                decimal balance = _service.GetBalanceByPeriod(startDate, endDate, _usuarioId);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao calcular saldo por período: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao calcular o saldo por período.");
            }
        }

        [HttpGet("list-by-period")]
        public ActionResult<IEnumerable<Transaction>> GetByPeriod( 
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("A data de início não pode ser maior que a data de fim.");
                }

                var transacoes = _service.GetByPeriod(startDate, endDate, _usuarioId);

                if (transacoes == null || !transacoes.Any())
                {
                    return NotFound("Nenhuma transação encontrada para o período especificado.");
                }

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar por período: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao listar as transações por período.");
            }
        }

        [HttpPost]
        public ActionResult<Transaction> Post([FromBody] Transaction transacao) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                transacao.UsuarioId = _usuarioId;

                var novaTransacao = _service.Add(transacao);

                return CreatedAtAction(nameof(Get), new { id = novaTransacao.Id }, novaTransacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar transação: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao criar a transação.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var transacao = _service.GetById(id, _usuarioId);

                if (transacao == null)
                {
                    return NotFound($"Transação com ID {id} não encontrada.");
                }

                _service.Delete(id, _usuarioId);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar transação: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao deletar a transação.");
            }
        }
    }
}
