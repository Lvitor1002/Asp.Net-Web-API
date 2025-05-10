using System.Drawing;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<FuncionarioModel> AdicionarFuncionario(FuncionarioModel funcionario);
        Task<List<FuncionarioModel>> BuscarTodosFuncionario(int numeroDaPagina, int quantidadeDados);

        Task<FuncionarioModel> BuscarUnicoFuncionario(int id); 

    }
}
