
//DTO | Data transfer object -> Objeto de transferência de dados

namespace Api.Controllers
{
    public class FuncionarioDto
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public IFormFile Foto { get; set; }
    }

}
