using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/enderecos")]
[ApiController]
public class EnderecoController : ControllerBase
{
    private readonly EnderecoRepositorio enderecoRepositorio;
    public EnderecoController(EnderecoRepositorio _enderecoRepositorio)
    {
        enderecoRepositorio = _enderecoRepositorio;
    }


    [HttpGet]
    public async Task<IResult> BuscarEnderecosCadastrados()
    {

        return Results.Ok();
    }
}
