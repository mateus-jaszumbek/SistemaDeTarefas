using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<TarefasModel>> BuscarTodasTarefas();
        Task<TarefasModel> BuscarPorId(int id);
        Task<TarefasModel> Adicionar(TarefasModel tarefas);
        Task<TarefasModel> Atualizar(TarefasModel tarefas, int id);
        Task<bool> Apagar(int id);
    }
}
