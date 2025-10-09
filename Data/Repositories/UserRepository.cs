using PigFinance.API.Models;
using PigFinance.Data.Context;

namespace PigFinance.Data.Repositories
{
    private readonly DataContext _db;

    public UserRepository(DataContext db)
    {
        _db = db;
    }

    public void CriarUsuario(Usuario usuario)
    {
        _db.Usuario.Add(usuario);
    }

    public Usuario GetUsuarioByEmail(string email)
    {
        return _db.Usuario
            .Select(usuario => usuario)
            .Where(Usuario => Usuario.Email.Equals(email))
            .First();
    }

   


    public Usuario FindById(Guid Id)
    {
        return _db.Usuario.Select(u => u)
            .Where(u => u.Id == Id)
            .First();
    }

    public List<Usuario> GetUsuario()
    {
        List<Usuario> result = _db.Usuario
            .Select(usuario => usuario)
            .ToList();

        return result;
    }


}



