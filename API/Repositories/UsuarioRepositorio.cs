using API.Context;
using API.Utils;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Repositories;

public class UsuarioRepositorio
{
    private readonly UserManager<IdentityUser> _context;
    private readonly IConfiguration _configuration;
    public UsuarioRepositorio(UserManager<IdentityUser> context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public void teste()
    {
        JsonWebToken.GerarToken(new ClaimsIdentity(), _configuration);
    }
}
