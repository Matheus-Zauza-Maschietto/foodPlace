using API.Context;

namespace API.Repositories;

public class CategoriaRepositorio
{
	private readonly FoodPlaceContext _context;
	public CategoriaRepositorio(FoodPlaceContext context)
	{
		_context = context;
    }
}
