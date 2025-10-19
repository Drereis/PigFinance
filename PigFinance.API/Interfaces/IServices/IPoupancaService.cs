using PigFinance.API.Models;

namespace PigFinance.API.Services 
{
    public interface IPoupancaService
    {
        IEnumerable<Poupanca> GetAll(int usuarioId);
        Poupanca? GetById(int id, int usuarioId);
        Poupanca Add(Poupanca poupanca);
        Poupanca Update(Poupanca poupanca);
        void Delete(int id, int usuarioId);
    }
}