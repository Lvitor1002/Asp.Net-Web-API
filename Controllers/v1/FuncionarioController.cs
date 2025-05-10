using Api.Models;
using Api.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        //Construtor
        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository  = funcionarioRepository;
        }

        // GET: Buscar ÚNICO usuários
        [Authorize]//<- Solicita senha
        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioModel>> BuscarUnicoFuncionario(int id)
        {
            FuncionarioModel unicoFuncionario = await _funcionarioRepository.BuscarUnicoFuncionario(id);
            return Ok(unicoFuncionario);
        }

        [Authorize]//<- Solicita senha
        [HttpPost]
        [Route("{id}/download")]
        public async Task<ActionResult> DownloadFoto(int id)
        {
            var unicoFuncionario = await _funcionarioRepository.BuscarUnicoFuncionario(id);
            if(unicoFuncionario == null || string.IsNullOrEmpty(unicoFuncionario.CaminhoDaFoto))
            {
                return NotFound();
            }

            var dados = System.IO.File.ReadAllBytes(unicoFuncionario.CaminhoDaFoto);
            return File(dados,"image/png");
        }


        // GET: Buscar TODOS usuários
        
        [HttpGet]
        public async Task<ActionResult<List<FuncionarioModel>>> ListarFuncionarios(int numeroDaPagina, int quantidadeDados)
        {
            List<FuncionarioModel> listaFuncionarios = await _funcionarioRepository.BuscarTodosFuncionario(numeroDaPagina, quantidadeDados);
            return Ok(listaFuncionarios);
        }

        // POST: Enviar dados ao servidor
        [Authorize] //<- Solicita senha
        [HttpPost]
        public async Task<ActionResult<FuncionarioModel>> AdicionarFuncionario([FromForm] FuncionarioDto funcionarioDto)
        {

            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }
            var caminho = Path.Combine(pasta, funcionarioDto.Foto.FileName);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await funcionarioDto.Foto.CopyToAsync(stream);
            }

            var funcionarioModel = new FuncionarioModel
            {
                Nome = funcionarioDto.Nome,
                Idade = funcionarioDto.Idade,
                CaminhoDaFoto = caminho
            };

            var funcionario = await _funcionarioRepository.AdicionarFuncionario(funcionarioModel);
            return Ok(funcionario);
        }


        // PUT: Atualizar dados
        // DELETE: Apagar dados
    }
}
