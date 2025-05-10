using Api.Data;
using Api.Models;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly ConexaoDB _conexaoDB;

        public FuncionarioRepository(ConexaoDB conexaoDB)
        {
            _conexaoDB = conexaoDB;
        }

        public async Task<FuncionarioModel> AdicionarFuncionario(FuncionarioModel funcionario)
        {
            await _conexaoDB.Funcionario.AddAsync(funcionario);
            await _conexaoDB.SaveChangesAsync();
            return funcionario;
        }

        public async Task<List<FuncionarioModel>> BuscarTodosFuncionario(int numeroDaPagina, int quantidadeDados)
        {
            //numeroDaPagina*quantidadePaginas <- Paginação. Trás exatamente o n° de páginas desejada
            return await _conexaoDB.Funcionario.Skip((numeroDaPagina - 1) * quantidadeDados)
                                               .Take(quantidadeDados)
                                               .ToListAsync();
        }

        public async Task<FuncionarioModel?> BuscarUnicoFuncionario(int id)
        {
            return await _conexaoDB.Funcionario.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
