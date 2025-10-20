using PigFinance.API.Models;
using PigFinance.API.Services;
using System.Collections.Generic;
using System.Linq;

namespace PigFinance.API.Services
{
    public class UserService : IUsuarioService
    {
        private static List<Usuario> _usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "Admin Teste", Cpf = "12345678901", Email = "admin@teste.com", Senha = "senha_secreta", TipoUsuario = "Admin" }
        };

        public IEnumerable<Usuario> GetAll()
        {
            return _usuarios;
        }

        public Usuario? GetById(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public Usuario Register(Usuario novoUsuario)
        {
            if (_usuarios.Any(u => u.Email.Equals(novoUsuario.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            novoUsuario.Id = _usuarios.Any() ? _usuarios.Max(u => u.Id) + 1 : 1;

            _usuarios.Add(novoUsuario);

            return novoUsuario;
        }

        public Usuario? Authenticate(string email, string senha)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                return null;
            }

            if (usuario.Senha == senha)
            {
                return usuario;
            }

            return null;
        }
    }
}
