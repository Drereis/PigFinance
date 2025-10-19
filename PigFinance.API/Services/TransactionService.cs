using PigFinance.API.Models;
using PigFinance.API.Services; 

namespace PigFinance.API.Services
{
    public class TransactionService : ITransactionService
    {
        private static List<Transaction> _transacoes = new List<Transaction>
        {
            new Transaction { Id = 1, Descricao = "Salário Mensal", Valor = 3500.00m, Data = DateTime.Now.AddDays(-5), UsuarioId = 1, Categoria = TipoCategoria.Outros },
            new Transaction { Id = 2, Descricao = "Aluguel", Valor = -1200.00m, Data = DateTime.Now.AddDays(-4), UsuarioId = 1, Categoria = TipoCategoria.Moradia }
        };

        public TransactionService()
        {
        }

        public decimal GetTotalBalance(int usuarioId)
        {
            return _transacoes.Where(t => t.UsuarioId == usuarioId).Sum(t => t.Valor);
        }

        public IEnumerable<Transaction> GetAll(int usuarioId)
        {
            return _transacoes.Where(t => t.UsuarioId == usuarioId);
        }

        public decimal GetBalanceByPeriod(DateTime startDate, DateTime endDate, int usuarioId)
        {
            return _transacoes
                .Where(t => t.UsuarioId == usuarioId && t.Data >= startDate && t.Data <= endDate)
                .Sum(t => t.Valor);
        }

        public IEnumerable<Transaction> GetByPeriod(DateTime startDate, DateTime endDate, int usuarioId)
        {
            return _transacoes
                .Where(t => t.UsuarioId == usuarioId && t.Data >= startDate && t.Data <= endDate)
                .ToList();
        }

        public Transaction? GetById(int id, int usuarioId)
        {
            return _transacoes.FirstOrDefault(t => t.Id == id && t.UsuarioId == usuarioId);
        }

        public Transaction Add(Transaction transacao)
        {
            transacao.Id = _transacoes.Any() ? _transacoes.Max(t => t.Id) + 1 : 1;
            _transacoes.Add(transacao);
            return transacao;
        }

        public void Delete(int id, int usuarioId)
        {
            var transacao = _transacoes.FirstOrDefault(t => t.Id == id && t.UsuarioId == usuarioId);

            if (transacao != null)
            {
                _transacoes.Remove(transacao);
            }
        }
    }
}
