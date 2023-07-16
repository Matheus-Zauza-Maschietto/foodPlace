using API.Context;
using API.Dtos;
using API.Models;
using API.Responses;

namespace API.Repositories;

public class ProdutoRepositorio : Repositorio
{
    public ProdutoRepositorio(FoodPlaceContext context): base(context)
    {}

    public ProdutoResponse CriarProdutoPorLoja(ProdutoDto produtoDto, int lojaId)
    {
        try
        {
            Produto produto = new Produto(produtoDto, lojaId);
            _context.Produto.Add(produto);
            _context.SaveChanges();
            return new ProdutoResponse(produto.Id, produto.Nome, produto.Descricao, produto.Disponivel, produto.Preco);
        }
        catch(Exception) 
        {
            produtoDto.AddNotification("Erro ao criar", "Erro ao criar produto");
            return new ProdutoResponse(Menssagem: "Não foi possivel criar ");
        }
    }

    public IEnumerable<ProdutoResponse> ListarProdutosDisponivelPorLoja(int lojaId)
    {
        var produtos = _context.Produto
                                .Where(produto => produto.Disponivel && produto.LojaId == lojaId)
                                .Select(produto => new ProdutoResponse(produto.Id, produto.Nome, produto.Descricao, produto.Disponivel, produto.Preco, ""));
        return produtos;
    }

    public IEnumerable<ProdutoResponse> ListarProdutosPorLoja(int lojaId)
    {
        var produtos = _context.Produto
                                .Where(produto => produto.LojaId == lojaId)
                                .Select(produto => new ProdutoResponse(produto.Id, produto.Nome, produto.Descricao, produto.Disponivel, produto.Preco, ""));
        return produtos;
    }

    public ProdutoResponse BuscarProdutoPorLojaPorId(int lojaId, Guid produtoId)
    {
        var produto = _context.Produto
                                .FirstOrDefault(produto => produto.LojaId == lojaId && produto.Id == produtoId);

        if(produto is null)
        {
            return new ProdutoResponse(Menssagem:"Produto Não encontrado");
        }

        return new ProdutoResponse(produto.Id, produto.Nome, produto.Descricao, produto.Disponivel, produto.Preco, "");
    }

    public ProdutoResponse DeletarProdutoPorIdComEmailUsuario(Guid produtoId, string emailUsuario)
    {
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
    
        var produto = _context.Produto.FirstOrDefault(produto => produto.Id == produtoId && produto.Loja.DonoId == usuario.Id);
        if(produto is null)
        {
            return new ProdutoResponse(Menssagem: "Produto não encontrado");
        }

        _context.Produto.Remove(produto);
        _context.SaveChanges();
        return new ProdutoResponse(Menssagem: "Produto deletado com sucesso");
    }

    public ProdutoResponse AtualizarProdutoPorIdComEmailUsuario(Guid produtoId, string emailUsuario, ProdutoDto produtoDto)
    {
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
        var produto = _context.Produto.FirstOrDefault(produto => produto.Id == produtoId && produto.Loja.DonoId == usuario.Id);
        if (produto is null)
        {
            return new ProdutoResponse(Menssagem: "Produto não encontrado");
        }

        produto.Atualizar(produtoDto);
        _context.Produto.Update(produto);
        _context.SaveChanges();
        return new ProdutoResponse(Menssagem: "Produto atualizado com sucesso");
    }

    public ProdutoResponse AtualizarProdutoPorIdComEmailUsuario(Guid produtoId, string emailUsuario, bool disponibilidade)
    {
        var usuario = BuscarUsuarioPorEmail(emailUsuario);
        var produto = _context.Produto.FirstOrDefault(produto => produto.Id == produtoId && produto.Loja.DonoId == usuario.Id);
        if (produto is null)
        {
            return new ProdutoResponse(Menssagem: "Produto não encontrado");
        }

        produto.Disponivel = disponibilidade;
        _context.Produto.Update(produto);
        _context.SaveChanges();
        return new ProdutoResponse(Menssagem: "Produto atualizado com sucesso");
    }
}
