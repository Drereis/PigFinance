using Microsoft.AspNetCore.Mvc;
using PigFinance.API.Models;
using PigFinance.PigFinance.API.Interfaces.IServices;
using System.Collections.Generic;
using System.Linq;

namespace PigFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get()
        {
            try
            {
                var transactions = _service.GetAll();

                if (transactions == null || !transactions.Any())
                {
                    return NotFound("Nenhuma transação encontrada.");
                }

                return Ok(transactions);
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
                var transaction = _service.GetById(id);

                if (transaction == null)
                {
                    return NotFound($"Transação com ID {id} não encontrada.");
                }

                return Ok(transaction);
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
                decimal balance = _service.GetTotalBalance();
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

                decimal balance = _service.GetBalanceByPeriod(startDate, endDate);
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

                var transactions = _service.GetByPeriod(startDate, endDate);

                if (transactions == null || !transactions.Any())
                {
                    return NotFound("Nenhuma transação encontrada para o período especificado.");
                }

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar por período: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao listar as transações por período.");
            }
        }

     
        [HttpPost]
        public ActionResult<Transaction> Post([FromBody] Transaction transaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newTransaction = _service.Add(transaction);

                return CreatedAtAction(nameof(Get), new { id = newTransaction.Id }, newTransaction);
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
                var transaction = _service.GetById(id);

                if (transaction == null)
                {
                    return NotFound($"Transação com ID {id} não encontrada.");
                }

                _service.Delete(id);

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