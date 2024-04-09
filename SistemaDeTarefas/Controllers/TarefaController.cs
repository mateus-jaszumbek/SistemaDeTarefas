using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefasModel>>> ListarTodas()
        {
            List<TarefasModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefasModel>> BuscarPorId(int id)
        {
            TarefasModel tarefa = await _tarefaRepositorio.BuscarPorId(id);
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<ActionResult<TarefasModel>> Cadastrar([FromBody] TarefasModel tarefaModel)
        {
            TarefasModel tarefa = await _tarefaRepositorio.Adicionar(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TarefasModel>> Atualizar([FromBody] TarefasModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefasModel tarefa = await _tarefaRepositorio.Atualizar(tarefaModel, id);
            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> Apagar(int id)
        {
            bool apagado = await _tarefaRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}
