using PigFinance.API.Models;

namespace PigFinance.PigFinance.API.Interfaces.IServices
{
    public interface ICategoryService
    {
        IEnumerable<Usuario> GetAll();
        Usuario? GetById(int id);
    }
}