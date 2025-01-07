using CleanArchMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMVC.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
    //definir opções do contexto
    //registrar o contexto como serviço, informando o provedor e a string de conexão
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    
    //mapeando as entidades para tabelas
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    //configurar o modelo usando a fluent api
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Percorre o projeto onde está a classe application db context
        //quando ele achar as classes de configuração que herdam IEntityTypeConfiguration, ele ja aplica automatico as configs
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        //uma outra forma seria referenciar automaticamente as classses de configuração
        //builder.AppltConfiguration(new CategoryConfiguration());
    }
}

