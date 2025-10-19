using PigFinance.API.Models;
using PigFinance.API.Services;
using System.Collections.Generic;
using System.Linq;

namespace PigFinance.API.Services
{
    public class PoupancaService : IPoupancaService
    {
        private static List<Poupanca> _poupancas = new List<Poupanca>
        {
            new Poupanca { Id = 1, Nome = "Férias 2026", ValorAlvo = 5000.00m, ValorAtual = 1200.00m, DataAlvo = DateTime.Now.AddMonths(12), UsuarioId = 1 },
            new Poupanca { Id = 2, Nome = "Novo Carro", ValorAlvo = 30000.00m, ValorAtual = 5000.00m, DataAlvo = DateTime.Now.AddMonths(36), UsuarioId = 1 }
        };

        public IEnumerable<Poupanca> GetAll(int usuarioId)
        {
            return _poupancas.Where(p => p.UsuarioId == usuarioId);
        }

        public Poupanca? GetById(int id, int usuarioId)
        {
            return _poupancas.FirstOrDefault(p => p.Id == id && p.UsuarioId == usuarioId);
        }

        public Poupanca Add(Poupanca poupanca)
        {
            poupanca.Id = _poupancas.Any() ? _poupancas.Max(p => p.Id) + 1 : 1;
            _poupancas.Add(poupanca);
            return poupanca;
        }

        public Poupanca Update(Poupanca poupanca)
        {
            var existing = GetById(poupanca.Id, poupanca.UsuarioId);
            if (existing == null) return null;

            existing.Nome = poupanca.Nome;
            existing.ValorAlvo = poupanca.ValorAlvo;
            existing.ValorAtual = poupanca.ValorAtual;
            existing.DataAlvo = poupanca.DataAlvo;

            return existing;
        }

        public void Delete(int id, int usuarioId)
        {
            var existing = GetById(id, usuarioId);
            if (existing != null)
            {
                _poupancas.Remove(existing);
            }
        }
    }
}
