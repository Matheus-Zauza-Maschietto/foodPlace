using API.Context;
using API.Models;
using API.Responses;

namespace API.Repositories;

public class CategoriaRepositorio : Repositorio
{
    public CategoriaRepositorio(FoodPlaceContext context) : base(context)
    {}
}
