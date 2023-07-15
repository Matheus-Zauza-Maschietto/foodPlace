using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace API.Context;

public class FoodPlaceContext: IdentityDbContext<IdentityUser>
{
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Loja> Loja { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<Categoria> Categoria { get; set; }

    public FoodPlaceContext(DbContextOptions<FoodPlaceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        _configurarManyToManyCategoriasProdutos(builder);
        _setarConstrainsLoja(builder);
        _setarConstrainsEndereco(builder);
        _setarConstrainsProduto(builder);
    }

    private void _configurarManyToManyCategoriasProdutos(ModelBuilder builder)
    {
        builder.Entity<ProdutoCategoria>()
            .HasKey(ad => new { ad.CategoriaId, ad.ProdutoId });

        builder.Entity<ProdutoCategoria>()
            .HasOne(ad => ad.Produto)
            .WithMany(a => a.Categorias)
            .HasForeignKey(ad => ad.ProdutoId);

        builder.Entity<ProdutoCategoria>()
            .HasOne(ad => ad.Categoria)
            .WithMany(d => d.Produtos)
            .HasForeignKey(ad => ad. CategoriaId);
    }

    private void _setarConstrainsLoja(ModelBuilder builder)
    {
        builder.Entity<Loja>().Property(p => p.Nome).HasMaxLength(128).IsRequired();
        builder.Entity<Loja>().Property(p => p.Telefone).HasMaxLength(16).IsRequired();
        builder.Entity<Loja>().Property(p => p.Email).HasMaxLength(256).IsRequired();
        builder.Entity<Loja>().Property(p => p.CNPJ).HasMaxLength(18).IsFixedLength().IsRequired();
    }

    private void _setarConstrainsEndereco(ModelBuilder builder)
    {
        builder.Entity<Endereco>().Property(p => p.Estado).IsRequired();
        builder.Entity<Endereco>().Property(p => p.Cidade).IsRequired();
        builder.Entity<Endereco>().Property(p => p.Rua).HasMaxLength(256).IsRequired();
        builder.Entity<Endereco>().Property(p => p.Numero).IsRequired();
        builder.Entity<Endereco>().Property(p => p.Complemento).HasMaxLength(512);
        builder.Entity<Endereco>().Property(p => p.CEP).HasMaxLength(10).IsFixedLength().IsRequired();
    }

    private void _setarConstrainsProduto(ModelBuilder builder)
    {
        builder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(128).IsRequired();
        builder.Entity<Produto>().Property(p => p.Preco).HasColumnType("decimal(10, 2)").IsRequired();
        builder.Entity<Produto>().Property(p => p.Disponivel).IsRequired();
    }
}
