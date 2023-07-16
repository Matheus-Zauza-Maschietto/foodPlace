using API.Dtos;
using API.Repositories;
using API.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("/lojas/{idLoja:int}/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly LojaRepositorio _lojaRepositorio;
        public ProdutoController(ProdutoRepositorio produtoRepositorio, LojaRepositorio lojaRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _lojaRepositorio = lojaRepositorio;
        }

        [HttpPost]
        public async Task<IResult> CriarProduto(ProdutoDto produtoDto, [FromRoute] int idLoja)
        {
            produtoDto.Validar();
            ResponseModel<ProdutoResponse> response;

            if (!produtoDto.IsValid)
            {
                response = new ResponseModel<ProdutoResponse>(produtoDto);
                return Results.BadRequest(response);
            }

            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);

            var loja = await _lojaRepositorio.BuscarLojaPorEmailPorId(emailUsuario, idLoja);
            if (loja.Id is null)
            {
                produtoDto.AddNotification("Loja", "Loja não encontrada");
                response = new ResponseModel<ProdutoResponse>(produtoDto);
                return Results.BadRequest(response);
            }

            ProdutoResponse produtoResponse = _produtoRepositorio.CriarProdutoPorLoja(produtoDto, idLoja);

            if (produtoDto.Notifications.Any())
            {
                response = new ResponseModel<ProdutoResponse>(produtoDto);
                return Results.BadRequest(response);
            }

            return Results.CreatedAtRoute($"/lojas/{idLoja}/produtos/{produtoResponse.Id}", new ResponseModel<ProdutoResponse>(produtoDto, produtoResponse));
        }

        [HttpGet()]
        public async Task<IResult> ListarProdutosPorLoja([FromRoute] int idLoja)
        {
            ResponseModel<IEnumerable<ProdutoResponse>> response;
            IEnumerable<ProdutoResponse> produtoResponse =  _produtoRepositorio.ListarProdutosPorLoja(idLoja);

            return Results.Ok(new ResponseModel<IEnumerable<ProdutoResponse>>(produtoResponse));
        }

        [HttpGet("{idProduto:Guid}")]
        public async Task<IResult> BuscarProdutoPorLoja([FromRoute] int idLoja, [FromRoute] Guid idProduto)
        {
            ResponseModel<ProdutoResponse> response;
            ProdutoResponse produtoResponse = _produtoRepositorio.BuscarProdutoPorLojaPorId(idLoja, idProduto);

            if(produtoResponse.Id is null)
            {
                return Results.BadRequest(new ResponseModel<ProdutoResponse>(produtoResponse));
            }

            return Results.Ok(new ResponseModel<ProdutoResponse>(produtoResponse));
        }

        [HttpDelete("{idProduto:Guid}")]
        public async Task<IResult> DeletarProdutoPorId([FromRoute] int idLoja, [FromRoute] Guid idProduto)
        {
            ResponseModel<ProdutoResponse> response;
            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);
            ProdutoResponse produtoResponse = _produtoRepositorio.DeletarProdutoPorIdComEmailUsuario(idProduto, emailUsuario);

            if (produtoResponse.Id is null)
            {
                return Results.BadRequest(new ResponseModel<ProdutoResponse>(produtoResponse));
            }

            return Results.Ok(new ResponseModel<ProdutoResponse>(produtoResponse));
        }

        [HttpPut("{idProduto:Guid}")]
        public async Task<IResult> AtualizarProduto([FromRoute] Guid idProduto, ProdutoDto produtoDto)
        {
            produtoDto.Validar();
            ResponseModel<ProdutoResponse> response;
            if (!produtoDto.IsValid)
            {
                response = new ResponseModel<ProdutoResponse>(produtoDto);
                return Results.BadRequest(response);
            }

            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);
            ProdutoResponse produtoResponse = _produtoRepositorio.AtualizarProdutoPorIdComEmailUsuario(idProduto, emailUsuario, produtoDto);

            if (produtoResponse.Id is null)
            {
                produtoDto.AddNotification("NotFound", "Produto was not found");
                return Results.BadRequest(new ResponseModel<ProdutoResponse>(produtoDto));
            }

            return Results.Ok(new ResponseModel<ProdutoResponse>(produtoDto, produtoResponse));
        }

        [HttpPatch()]
        public async Task<IResult> AtualizarDisponibilidadeProduto([FromRoute] Guid idProduto, bool disponibilidade)
        {
            ResponseModel<ProdutoResponse> response;
            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);
            ProdutoResponse produtoResponse = _produtoRepositorio.AtualizarProdutoPorIdComEmailUsuario(idProduto, emailUsuario, disponibilidade);

            if (produtoResponse.Id is null)
            {
                return Results.BadRequest(new ResponseModel<ProdutoResponse>(produtoResponse));
            }

            return Results.Ok(new ResponseModel<ProdutoResponse>(produtoResponse));
        }

    }
}
