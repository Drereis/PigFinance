using PigFinance.API.Models;
using System.Collections.Generic;

namespace PigFinance.API.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll(int usuarioId);
        decimal GetTotalBalance(int usuarioId);
        decimal GetBalanceByPeriod(DateTime startDate, DateTime endDate, int usuarioId);
        IEnumerable<Transaction> GetByPeriod(DateTime startDate, DateTime endDate, int usuarioId);
        Transaction? GetById(int id, int usuarioId);
        Transaction Add(Transaction transacao);
        void Delete(int id, int usuarioId);
    }
}