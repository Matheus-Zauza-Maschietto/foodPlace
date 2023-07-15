using API.Context;

namespace API.Repositories;

public class LojaRepositorio
{
    private readonly FoodPlaceContext _context;
    public LojaRepositorio(FoodPlaceContext context)
    {
        _context = context;
    }
}
