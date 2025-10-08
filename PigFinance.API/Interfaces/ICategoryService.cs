using PigFinance.API.Models;

namespace PigFinance.API.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Usuario> GetAll();
        Usuario? GetById(int id);
    }
}