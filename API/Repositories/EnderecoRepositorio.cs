using API.Context;

namespace API.Repositories;

public class EnderecoRepositorio
{
    private readonly FoodPlaceContext _context;
    public EnderecoRepositorio(FoodPlaceContext context)
    {
        _context = context;
    }
}
