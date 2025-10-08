using PigFinance.API.Models;
using System.Collections.Generic;

namespace PigFinance.API.Services
{
    public interface IUsuarioService
    {

        IEnumerable<Usuario> GetAll();

       
        Usuario? GetById(int id);

        
        Usuario Register(Usuario usuario);


        Usuario? Authenticate(string email, string senha);
    }
}
