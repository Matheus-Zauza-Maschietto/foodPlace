using API.Context;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories;

public class Repositorio
{
    protected readonly FoodPlaceContext _context;
    public Repositorio(FoodPlaceContext context)
    {
        _context = context;

    }

    public virtual IdentityUser BuscarUsuarioPorEmail(string emailUsuario)
    {
        return _context.Users.FirstOrDefault(p => p.Email == emailUsuario);
    }
}
