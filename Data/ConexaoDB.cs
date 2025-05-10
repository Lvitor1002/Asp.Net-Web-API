

//             EntityFrameworkCore = ORM -> mapeia tabelas do banco como classes C#.

using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Data.Map;

namespace Api.Data
{
    public class ConexaoDB : DbContext
    {
        //Construtor
        public ConexaoDB(DbContextOptions<ConexaoDB> options) : base(options) { 
        }

        //Tabela Funcionario 
        public DbSet<FuncionarioModel> Funcionario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    
}
