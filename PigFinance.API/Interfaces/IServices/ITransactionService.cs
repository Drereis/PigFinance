using PigFinance.API.Models;

namespace PigFinance.PigFinance.API.Interfaces.IServices
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();
        decimal GetTotalBalance();
            
        decimal GetBalanceByPeriod(DateTime startDate, DateTime endDate);

        IEnumerable<Transaction> GetByPeriod(DateTime startDate, DateTime endDate);
        Transaction? GetById(int id);
        Transaction Add(Transaction transaction);
        void Delete(int id);
    }
}