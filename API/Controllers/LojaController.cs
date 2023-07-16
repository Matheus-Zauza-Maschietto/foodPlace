using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/")]
    [Authorize]
    [ApiController]
    public class LojaController : ControllerBase
    {
        [HttpPost("lojas")]
        public async Task<IResult> CriarLoja(LojaDto lojaDto)
        {
            return Results.Ok();
        }
    }
}
