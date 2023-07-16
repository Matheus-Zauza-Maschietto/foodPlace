using API.Context;
using API.Dtos;
using API.Models;
using API.Responses;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories;

public class LojaRepositorio : Repositorio
{
    public LojaRepositorio(FoodPlaceContext context) : base(context)
    {}

    public LojaResponse CriarLoja(LojaDto lojaDto, string emailUsuario)
    {
        try
        {
            var usuario = BuscarUsuarioPorEmail(emailUsuario);
            Loja loja = new Loja(lojaDto, usuario.Id);
            _context.Loja.Add(loja);
            _context.SaveChanges();
            return new LojaResponse(loja.Id, loja.Nome, loja.CNPJ, loja.Telefone, loja.Email);
        }
        catch(Exception)
        {
            return new LojaResponse(Mensagem: "Houve um erro durante a criação, tente novamente mais tarde.");
        }
    }

    public int BuscarQuantidadeDeLojaPorUsuario(string emailUsuario)
    {
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
        return _context.Loja.Where(loja => loja.DonoId == usuario.Id).Count();
    }

    public async Task<IEnumerable<LojaResponse>> ListarLojasPorEmail(string emailUsuario)
    {
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
        var lojas = _context.Loja.Where(loja => loja.DonoId == usuario.Id).OrderBy(loja => loja.Nome);
        return lojas.Select(loja => new LojaResponse(loja.Id, loja.Nome, loja.CNPJ, loja.Telefone, loja.Email, ""));
    }

    public async Task<LojaResponse> BuscarLojaPorEmailPorId(string emailUsuario, int id)
    {
        LojaResponse lojaResponse;
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
        var loja = _context.Loja.FirstOrDefault(loja => loja.DonoId == usuario.Id && loja.Id == id);
        
        if(loja is null)
        {
            return new LojaResponse(Mensagem: "Loja não encontrada");
        }

        return new LojaResponse(loja.Id, loja.Nome, loja.CNPJ, loja.Telefone, loja.Email, "Loja encontrada com sucesso");
    }

    public LojaResponse DeletarLojaPorEmailPorId(string emailUsuario, int id)
    {
        LojaResponse lojaResponse;
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
        var loja = _context.Loja.FirstOrDefault(loja => loja.DonoId == usuario.Id && loja.Id == id);

        _context.Loja.Remove(loja);
        _context.SaveChanges();

        return new LojaResponse(Mensagem: "Loja deletada com sucesso");
    }

    public LojaResponse AtualizarLojaPorEmailPorId(string emailUsuario, int id, LojaDto lojaDto)
    {
        LojaResponse lojaResponse;
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
        var loja = _context.Loja.FirstOrDefault(loja => loja.DonoId == usuario.Id && loja.Id == id);

        if (loja is null)
        {
            return new LojaResponse(Mensagem: "Loja não encontrada");
        }

        loja.Atualizar(lojaDto);
        _context.Loja.Update(loja);
        _context.SaveChanges();

        return new LojaResponse(loja.Id, loja.Nome, loja.CNPJ, loja.Telefone, loja.Email, "Loja atualizada com sucesso");
    }
}
