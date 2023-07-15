using API.Context;

namespace API.Repositories;

public class ProdutoRepositorio
{
    private readonly FoodPlaceContext _context;
    public ProdutoRepositorio(FoodPlaceContext context)
    {
        _context = context;
    }
}
