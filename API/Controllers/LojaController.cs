using API.Dtos;
using API.Models;
using API.Repositories;
using API.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("/lojas")]
    [Authorize]
    [ApiController]
    public class LojaController : ControllerBase
    {
        private readonly LojaRepositorio _repositorio;
        public LojaController(LojaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost()]
        public async Task<IResult> CriarLoja(LojaDto lojaDto)
        {
            lojaDto.Validar();
            ResponseModel<LojaResponse> response;

            if (!lojaDto.IsValid)
            {
                response = new ResponseModel<LojaResponse>(lojaDto);
                return Results.BadRequest(response);
            }

            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);

            if (_repositorio.BuscarQuantidadeDeLojaPorUsuario(emailUsuario) > 2)
            {
                lojaDto.AddNotification("Max Capacite", "Maximum number of stores per user reached");
                response = new ResponseModel<LojaResponse>(lojaDto);
                return Results.UnprocessableEntity(response);
            }

            LojaResponse lojaResponse = _repositorio.CriarLoja(lojaDto, emailUsuario);

            if (lojaResponse.Id is null)
            {
                lojaDto.AddNotification("Creation Error", "there was an error in creation");
                response = new ResponseModel<LojaResponse>(lojaDto);
                return Results.UnprocessableEntity(response);
            }

            response = new ResponseModel<LojaResponse>(lojaDto, lojaResponse);
            return Results.CreatedAtRoute($"/lojas/{lojaResponse.Id}", response);
        }

        [HttpGet()]
        public async Task<IResult> ListarLojas()
        {
            ResponseModel<IEnumerable<LojaResponse>> response;
            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);

            IEnumerable<LojaResponse> lojas = await _repositorio.ListarLojasPorEmail(emailUsuario);

            response = new ResponseModel<IEnumerable<LojaResponse>>(lojas);
            return Results.Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IResult> BuscarLojaPorUsuarioPorId([FromRoute] int id)
        {
            ResponseModel<LojaResponse> response;

            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);

            LojaResponse loja = await _repositorio.BuscarLojaPorEmailPorId(emailUsuario, id);
            response = new ResponseModel<LojaResponse>(loja);

            if(loja.Id is null)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IResult> DeletarLojaPorUsuarioPorId([FromRoute] int id)
        {
            ResponseModel<LojaResponse> response;
            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);

            LojaResponse loja = _repositorio.DeletarLojaPorEmailPorId(emailUsuario, id);
            response = new ResponseModel<LojaResponse>(loja);

            return Results.Ok(response);
        }


        [HttpPut("{id:int}")]
        public async Task<IResult> AtualizarLojaPorUsuarioPorId([FromRoute] int id, LojaDto lojaDto)
        {

            ResponseModel<LojaResponse> response;
            lojaDto.Validar();
            if (!lojaDto.IsValid)
            {
                response = new ResponseModel<LojaResponse>(lojaDto, null);
                return Results.BadRequest(response);
            }

            string emailUsuario = User.FindFirstValue(ClaimTypes.Email);

            LojaResponse loja =  _repositorio.AtualizarLojaPorEmailPorId(emailUsuario, id, lojaDto);
            response = new ResponseModel<LojaResponse>(loja);

            return Results.Ok(response);
        }
    }
}
