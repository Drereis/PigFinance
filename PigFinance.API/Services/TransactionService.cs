using PigFinance.API.Models;
using PigFinance.PigFinance.API.Interfaces.IServices;

namespace PigFinance.API.Services
{
    public class TransactionService : ITransactionService
    {
   
        private readonly ICategoryService _categoryService;

     
        private static List<Transaction> _transactions = new List<Transaction>
        {
          
            new Transaction { Id = 1, Descricao = "Salário Mensal", Amount = 3500.00m, Data = DateTime.Now.AddDays(-5), CategoriaId = 1 }, 
            new Transaction { Id = 2, Descricao = "Aluguel", Amount = -1200.00m, Data = DateTime.Now.AddDays(-4), CategoriaId = 2 } 
        };

        public TransactionService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

      
        private void PopulateCategory(Transaction transaction)
        {
            if (transaction != null)
            {
                transaction.TipoCategoria = _categoryService.GetById(transaction.CategoriaId);
            }
        }

        public decimal GetTotalBalance()
        {
            return _transactions.Sum(t => t.Amount);
        }

        public IEnumerable<Transaction> GetAll()
        {
            foreach (var t in _transactions)
            {
                PopulateCategory(t);
            }
            return _transactions;
        }

        public decimal GetBalanceByPeriod(DateTime startDate, DateTime endDate)
        {
            return _transactions
                .Where(t => t.Data >= startDate && t.Data <= endDate)
                .Sum(t => t.Amount);
        }

        public IEnumerable<Transaction> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            var transactionsInPeriod = _transactions
                .Where(t => t.Data >= startDate && t.Data <= endDate)
                .ToList();

            foreach (var t in transactionsInPeriod)
            {
                PopulateCategory(t);
            }

            return transactionsInPeriod;
        }

        public Transaction? GetById(int id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == id);
            PopulateCategory(transaction); 
            return transaction;
        }

        public Transaction Add(Transaction transaction)
        {
            transaction.Id = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1;

            _transactions.Add(transaction);
            PopulateCategory(transaction);

            return transaction;
        }
        public void Delete(int id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == id);

            if (transaction != null)
            {
                _transactions.Remove(transaction);
            }
        }
    }
}