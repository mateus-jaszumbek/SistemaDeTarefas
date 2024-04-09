using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemasTarefasDBContext _dbContext;

        public TarefaRepositorio(SistemasTarefasDBContext sistemasTarefasDBContext)
        {
            _dbContext = sistemasTarefasDBContext;
        }

        public async Task<TarefasModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefasModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public async Task<TarefasModel> Adicionar(TarefasModel tarefas)
        {
            await _dbContext.Tarefas.AddAsync(tarefas);
            await _dbContext.SaveChangesAsync();

            return tarefas;
        }

        public async Task<TarefasModel> Atualizar(TarefasModel tarefas, int id)
        {
            TarefasModel tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID {id} não encontrado.");
            }

            tarefaPorId.Nome = tarefas.Nome;
            tarefaPorId.Descricao = tarefas.Descricao;
            tarefaPorId.Status = tarefas.Status;
            tarefaPorId.UsuarioId = tarefas.UsuarioId;

            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefasModel tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa por o ID {id} não encontrado.");
            }

            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
