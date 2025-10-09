using PigFinance.API.Models;
using System.Collections.Generic;

namespace PigFinance.PigFinance.API.Interfaces.IServices
{
    public interface IUserService
    {

        IEnumerable<Usuario> GetAll();

       
        Usuario? GetById(int id);

        
        Usuario Register(Usuario usuario);


        Usuario? Authenticate(string email, string senha);
    }
}
